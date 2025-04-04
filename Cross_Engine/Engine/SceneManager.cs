﻿namespace CrossEngine.Engine
{
    public class SceneManager
    {
        List<Scene> scenes;
        public Scene ?CurrentScene;
        List<Scene> passiveScenes;
        Game game;

        public SceneManager(Game game)
        {
            scenes = new List<Scene>();
            passiveScenes = new List<Scene>();
            this.game = game;
        }

        public void ChangeScene(Scene scene)
        {
            if (CurrentScene != null)
            {
                Log.Print("Scene: " + CurrentScene.Name + " ended");
            }

            CurrentScene = scene;

            if (game.usingLua) game.luaState["c_console"] = CurrentScene.GetConsole();
            
            CurrentScene.Start();
            Log.Print("Started scene: " + scene.Name);
        }

        public void AddScene(Scene scene)
        {
            scenes.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {
            scene.DeleteScene(); // Clean lua files
            scenes.Remove(scene);
        }

        public void AddPassiveScene(Scene scene)
        {
            passiveScenes.Add(scene);
        }

        public void RemovePassiveScene(Scene scene)
        {
            passiveScenes.Remove(scene);
        }

        public List<Scene> GetPassiveScenes()
        {
            return passiveScenes;
        }

        public List<Scene> GetScenes()
        {
            return scenes;
        }

        public Scene GetScene(string name)
        {
            foreach(Scene scene in scenes)
            {
                if (scene.Name == name) return scene;
            }

            throw new Exception("Scene not found.");
        }

        public void Update()
        {
            foreach(Scene scene in scenes)
            {
                if (CurrentScene == scene) scene.Update();
            }

            foreach(Scene pScene in passiveScenes)
            {
                pScene.Update();
            }
        }

    }
}
