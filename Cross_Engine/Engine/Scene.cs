﻿using Cross_Engine;
using NLua;

namespace CrossEngine.Engine
{
    public class Scene
    {
        public string LUA_DIR = "";
        const int DRAW_LAYERS = 10;

        public string Name;
        public Game game;

        protected List<Sprite> ?sprites;
        protected List<List<Sprite>> ?DrawPriorityLayers;
        private TileMap ?tilemap;
        protected List<View>? views;
        private ConsoleDisplay inGameConsole;

        public Scene(Game game, string name)
        {
            Name = name;
            this.game = game;

            DrawPriorityLayers = new List<List<Sprite>>();
            for(int i = 0; i < DRAW_LAYERS; i++)
            {
                DrawPriorityLayers.Add(new List<Sprite>());
            }

            inGameConsole = new ConsoleDisplay(game, name + "_console", 0, 0, 100, 100);
            LinkConsole(inGameConsole);
            if (game.usingLua) CreateLuaFiles();
        }

        public void LoadLua()
        {
            try
            {
                if (File.Exists(LUA_DIR + "create.lua"))
                {
                    game.luaCode.Append(Environment.NewLine + "function scene_" + Name + "Create()" + Environment.NewLine);
                    game.luaCode.Append(File.ReadAllText(LUA_DIR + "create.lua") + Environment.NewLine);
                    game.luaCode.Append(Environment.NewLine + "end" + Environment.NewLine);
                }

                if (File.Exists(LUA_DIR + "update.lua"))
                {
                    game.luaCode.Append(Environment.NewLine + "function scene_" + Name + "Update(dt)" + Environment.NewLine);
                    game.luaCode.Append(File.ReadAllText(LUA_DIR + "update.lua") + Environment.NewLine);
                    game.luaCode.Append(Environment.NewLine + "end" + Environment.NewLine);
                }
            }         
            catch(Exception)
            {
                Log.Print("Failed to load lua files: " + Name);
            }
        }

        private void CreateLuaFiles()
        {
            LUA_DIR = game.LUA_ROOT + "scenes\\" + Name + "\\";
            if (!Directory.Exists(LUA_DIR))
            {
                Directory.CreateDirectory(LUA_DIR);
            }

            foreach(string luaFile in Program.LuaSceneFiles)
            {
                if(!File.Exists(LUA_DIR + luaFile)) File.Create(LUA_DIR + luaFile);
            }
        }

        private void DeleteLuaFiles()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(LUA_DIR);
                if (dir.Exists)
                {
                    dir.Delete(true);
                }
            }
            catch (IOException)
            {
                Log.ThrowException("Clean of " + Name + " scene lua files failed.");
            }
        }

        public void DeleteScene()
        {
            DeleteLuaFiles();
        }

        public void LinkConsole(ConsoleDisplay console)
        {
            inGameConsole = console;
        }

        public ConsoleDisplay GetConsole()
        {
            return inGameConsole;
        }

        public void SetTilemap(Game game, string asset, XYi tileSize, int[] tiles, int width, int height)
        {
            tilemap = new TileMap(game, asset);
            tilemap.Load(tileSize, tiles, width, height);
        }

        public void AddTilemapToView(View view)
        {
            if (tilemap == null) return;
            tilemap.AddToView(view);
        }

        public List<List<Sprite>> GetDrawLayers()
        {
            if (DrawPriorityLayers == null) throw new NullReferenceException();
            return DrawPriorityLayers;
        }

        public TileMap ?GetTilemap()
        {
            return tilemap;
        }

        /*
         * Layer :: Higher the value, lower the priority
         */
        protected void AddSprite(Sprite sprite, int layer)
        {
            if (layer >= DRAW_LAYERS)
            {
                Log.Error("Drawing Sprite: " + sprite.Name + " low prioity since layer greater than DRAW_LAYERS");
                layer = DRAW_LAYERS - 1;
            }

            if (DrawPriorityLayers == null) throw new NullReferenceException();
            DrawPriorityLayers[layer].Add(sprite);
        }

        protected void RemoveSprite(Sprite sprite)
        {
            if (DrawPriorityLayers == null) throw new NullReferenceException();
            
            int layerFnd = -1;
            for(int layer = 0; layer < DRAW_LAYERS; layer++)
            {
                foreach(Sprite spr in DrawPriorityLayers[layer])
                {
                    if (spr == sprite)
                    {
                        layerFnd = layer;
                        break;
                    }
                }
            }

            if (layerFnd == -1) return;

            DrawPriorityLayers[layerFnd].Remove(sprite);
        }

        // ============================================================================================
        // Functions with LUA CALLS
        // ============================================================================================
        public void Start()
        {
            if (game.usingLua)
            {
                var startFunc = game.luaState["scene_" + Name + "Create"] as LuaFunction;
                if (startFunc != null) startFunc.Call();
            }
        }

        public void Update()
        {
            if (DrawPriorityLayers == null) throw new NullReferenceException();

            if (inGameConsole != null) inGameConsole.Update();

            for (int layer = 0; layer < DRAW_LAYERS; layer++)
            {
                foreach (Sprite sprite in DrawPriorityLayers[layer])
                {
                    sprite.Update();
                    if (sprite != null && sprite.GetDrawable() != null)
                    {

                        for (int i = 0; i < sprite.GetViews().Count; i++)
                        {
                            KeyValuePair<View, XYf> viewAndPos = sprite.GetViews().ElementAt(i);
                            if (game == null || game.gameWindow == null)
                            {
                                throw new NullReferenceException();
                            }

                            game.gameWindow.SetView(viewAndPos.Key);
                            viewAndPos.Key.Update();
                        }
                    }
                }
            }

            if (game.usingLua)
            {
                var updateFunc = game.luaState["scene_" + Name + "Update"] as LuaFunction;
                if (updateFunc != null) updateFunc.Call(game.DeltaTime);
            }
        }
    }
}
