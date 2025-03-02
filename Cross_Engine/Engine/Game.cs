using Cross_Engine;
using SFML.System;
using SFML.Window;
using System.Reflection.Metadata;

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
                    FpsTimer.Restart();
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

        public Game(uint windowWidth, uint windowHeight, string gameName, Keyboard.Key endKey, bool handle = false)
        {
            if (!handle)
            {
                gameWindow = new Window(this, windowWidth, windowHeight, gameName, endKey, handle);
            }
            sceneManager = new SceneManager();
            inputHandler = new InputHandler();

        }

        public void RebuildWindow(uint windowWidth, uint windowHeight, string gameName, Keyboard.Key endKey, nint handle)
        {
            Handle = handle;
            gameWindow = new Window(this, windowWidth, windowHeight, gameName, endKey, true);
            gameWindow.RebuildWindow(handle);
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
                            catch(Exception ex)
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
