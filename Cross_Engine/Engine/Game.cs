using Cross_Engine;
using Cross_Engine.Engine;
using NLua;
using SFML.System;
using SFML.Window;
using System.Reflection.Metadata;
using System.Text;

namespace CrossEngine.Engine
{
    public class Game
    {
        // IMPORTANT
        public Window ?gameWindow = null;
        private SceneManager sceneManager;
        private InputHandler inputHandler;
        private Thread ?updateThread;
        private nint Handle;
        private bool _paused = false;
        public Lua luaState = new Lua();
        public StringBuilder luaCode = new StringBuilder();
        public string LUA_ROOT = "";
        public bool usingLua;
        public string Name;

        public bool Paused
        {
            get
            {
                return _paused;
            }

            set
            {
                _paused = value;

                if (!_paused && FpsTimer != null)
                {
                    FpsTimer = new Clock();
                    FpsTimer.Restart();
                    CountedFrames = 0;
                }
            }
        }

        // Secondary
        public float DeltaTime;
        public float FPS = 0f;
        float lastTime = DateTime.Now.Ticks;
        private float LOW_LIMIT = 0.0167f;
        private float HIGH_LIMIT = 0.1f;
        private Int64 CountedFrames = 0;
        private Clock ?FpsTimer;
        public bool IsRunning = false;

        public Game(uint windowWidth, uint windowHeight, string gameName, Keyboard.Key endKey, bool handle = false, bool usingLua = true)
        {
            if (!handle)
            {
                gameWindow = new Window(this, windowWidth, windowHeight, gameName, endKey, handle);
            }

            Name = gameName;
            sceneManager = new SceneManager(this);
            inputHandler = new InputHandler();
            this.usingLua = usingLua;

            if (this.usingLua)
            {
                LUA_ROOT = Directory.GetCurrentDirectory() + "\\Lua\\" + Program.ProjectName + "\\";

                if (!Directory.Exists(LUA_ROOT)) Directory.CreateDirectory(LUA_ROOT);
                if (!File.Exists(LUA_ROOT + "gameStart.lua")) File.Create("gameStart.lua");
            }

            LoadResources();
        }

        void LoadResources()
        {
            // Load default font if found
            try
            {
                Common.defaultFont = new SFML.Graphics.Font("Assets/fonts/default.ttf");
            }
            catch (Exception ex)
            {
                Log.Error("Default font could not be loaded. Make sure bin has Assets/fonts/default.ttf in it. These files are found in Engine folder.");
                throw new Exception();
            }
        }

        public void RebuildWindow(uint windowWidth, uint windowHeight, string gameName, Keyboard.Key endKey, nint handle)
        {
            Handle = handle;
            gameWindow = new Window(this, windowWidth, windowHeight, gameName, endKey, true);
            gameWindow.RebuildWindow(handle);
            Name = gameName;
        }

        public void RebuildWindow(uint windowWidth, uint windowHeight, string gameName, Keyboard.Key endKey)
        {
            gameWindow = new Window(this, windowWidth, windowHeight, gameName, endKey, false);
        }

        private void Init()
        {
            if (gameWindow == null)
            {
                Log.Error("Could not init Cross Engine. Null window.");
                return;
            }

            gameWindow.Init(SFML.Graphics.Color.Black);
            FpsTimer = new Clock();
            FpsTimer.Restart();

            Log.InfoPrintErrorLog(Name + " Initalized");

            if(usingLua) InitLua();
        }

        private void InitLua()
        {
            luaCode = new StringBuilder();
            luaState = new Lua();
            luaState.LoadCLRPackage();

            luaState["c_game"] = this;
            luaState["c_window"] = gameWindow;
            luaState["c_scene_manager"] = sceneManager;

            if (File.Exists(Environment.CurrentDirectory + "\\Lua\\" + "Lib.lua"))
            {
                string lib = File.ReadAllText(Environment.CurrentDirectory + "\\Lua\\" + "Lib.lua");
                luaCode.Append(lib + Environment.NewLine);
            }
            else
            {
                Log.ThrowException("Could not load/find lua library.");
                return;
            }

            if (File.Exists(LUA_ROOT + "gameStart.lua"))
            {
                luaCode.Append(Environment.NewLine + "function gameStart()" + Environment.NewLine);
                luaCode.Append(File.ReadAllText(LUA_ROOT + "gameStart.lua") + Environment.NewLine);
                luaCode.Append(Environment.NewLine + "end" + Environment.NewLine);
            }

            // Init scene lua
            foreach(Scene scene in sceneManager.GetScenes())
            {
                scene.LoadLua();
            }

            foreach(Scene scene in sceneManager.GetPassiveScenes())
            {
                scene.LoadLua();
            }

            // Create lua state
            try
            {
                luaState.DoString(luaCode.ToString());
            }
            catch(Exception ex)
            {
                Log.ThrowException(ex.ToString());
                return;
            }

            Log.InfoPrintErrorLog(Name + ": Lua Initialized");
        }

        public void Start(Scene startScene)
        {
            Init();
            sceneManager.ChangeScene(startScene);
            IsRunning = true;

            updateThread = new Thread(new ThreadStart(Update));
            updateThread.Start();
        }

        public void Start()
        {
            Init();
            IsRunning = true;

            updateThread = new Thread(new ThreadStart(Update));
            updateThread.Start();

            Log.InfoPrintErrorLog(Name + ": Started. Now Running.");
        }

        public void AddScene(Scene scene)
        {
            sceneManager.AddScene(scene);
        }

        public void RemoveScene(Scene scene)
        {
            sceneManager.RemoveScene(scene);
        }
        public void AddPassiveScene(Scene scene)
        {
            sceneManager.AddPassiveScene(scene);
        }
        public void RemovePassiveScene(Scene scene)
        {
            sceneManager.RemovePassiveScene(scene);
        }

        public List<Scene> GetScenes()
        {
            return sceneManager.GetScenes();
        }

        public Scene ?GetCurrentScene()
        {
            return sceneManager.CurrentScene;
        }

        public Scene GetScene(string name)
        {
            return sceneManager.GetScene(name);
        }

        private void Update()
        {
            if (gameWindow == null)
            {
                Log.Error("Cannot update game. Window null.");
                End();
                return;
            }

            while(IsRunning)
            {
                if (!gameWindow.Running) break;

                Application.DoEvents();
                if (Paused) continue;

                UpdateDeltaTime();

                // Update inputs
                inputHandler.Update();
                gameWindow.Update();
                sceneManager.Update();
                inputHandler.ClearJustPressed();

                RenderGames();
            }

            End();
        }

        private void UpdateDeltaTime()
        {
            if (FpsTimer != null)
            {
                float sec = FpsTimer.ElapsedTime.AsSeconds();
                float avgFPS = (float)(CountedFrames / (sec));
                if (avgFPS > 2000000)
                {
                    avgFPS = 0;
                }
                
                FPS = avgFPS;
            }


            float currentTime = DateTime.Now.Ticks;
            float deltaTime = (currentTime - lastTime) / 1000.0f;
            if (deltaTime < LOW_LIMIT)
                deltaTime = LOW_LIMIT;
            else if (deltaTime > HIGH_LIMIT)
                deltaTime = HIGH_LIMIT;

            lastTime = currentTime;
            CountedFrames++;
        }

        public void RenderGames()
        {
            if(gameWindow != null)
            {
                if (Program.WorldMaker != null && Handle == Program.WorldMaker.Handle)
                {
                    if (Program.Editor != null)
                    {
                        if (Program.Editor.Visible && Program.Editor.worldEditorSurface != null && IsRunning)
                        {
                            try
                            {
                                Program.Editor.worldEditorSurface.Invoke((MethodInvoker)delegate
                                {
                                    gameWindow.Render();
                                });
                            }
                            catch(Exception ex)
                            {
                                // Ignore
                            }
                        }
                    }
                }

                if (Program.CrossGame != null && Program.CrossGame.Handle == Handle)
                {
                    if (Program.CrossForm != null && Program.CrossForm.Visible && Program.CrossSurface != null && IsRunning)
                    {
                        if (!Program.CrossSurface.IsDisposed)
                        {
                            try
                            {
                                Program.CrossSurface.Invoke((MethodInvoker)delegate
                                {
                                    gameWindow.Render();
                                });
                            }
                            catch(Exception)
                            {
                                // Ignore
                            }
                        }
                    }
                }

                if (Program.SandboxGame != null && Program.SandboxGame.Handle == Handle)
                {
                    if (Program.SandboxForm != null && Program.SandboxForm.Visible && Program.SandboxSurface != null && IsRunning)
                    {
                        if (!Program.SandboxSurface.IsDisposed)
                        {
                            try
                            {
                                Program.SandboxSurface.Invoke((MethodInvoker)delegate
                                {
                                    gameWindow.Render();
                                });
                            }
                            catch (Exception)
                            {
                                // Ignore
                            }
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void End()
        {
            Log.Print("Cross Engine shutdown.");
            if (gameWindow != null) gameWindow.End();
        }

        public SceneManager GetSceneManager()
        {
            return sceneManager;
        }

        public InputHandler GetInputHandler()
        {
            return inputHandler;
        }

    }
}
