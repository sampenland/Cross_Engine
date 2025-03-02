using Cross_Engine;
using Cross_Engine.Designer;
using SFML.Graphics;
using SFML.Window;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace CrossEngine.Engine
{
    public class Window
    {
        private Game game;
        private RenderWindow ?mainWindow;
        private Keyboard.Key endKey = Keyboard.Key.Escape;
        DrawingSurface ?surface;
        Form? parentForm;
        public bool Running = false;

        public int Width;
        public int Height;

        public Window(Game game, uint width, uint height, string name, Keyboard.Key quitKey = Keyboard.Key.Escape, bool handle = false)
        {
            this.game = game;

            Width = (int)width;
            Height = (int)height;

            if (!handle)
            {
                mainWindow = new RenderWindow(new VideoMode(width, height), name);
            }

            if (mainWindow != null)
            {
                mainWindow.KeyPressed += WindowKeyPressSFML;
                mainWindow.Closed += WindowClosedSFML;
                mainWindow.KeyReleased += WindowKeyReleasedSFML;
            }

            endKey = quitKey;
        }

        public void RebuildWindow(nint handle)
        {
            mainWindow = new RenderWindow(handle);

            try
            {
                surface = Control.FromHandle(handle) as DrawingSurface;
                if (surface != null)
                {
                    surface.KeyPress += WindowKeyPress;
                    surface.KeyUp += WindowKeyReleased;
                    surface.Click += SurfaceClick;
                    
                    parentForm = surface.Parent as Form;
                    if (parentForm != null)
                    {
                        parentForm.FormClosed += WindowClosed;
                        parentForm.Click += FormClick;
                    }
                }

            }
            catch(Exception ex)
            {
                throw new Exception("Could not link input handler." + ex.ToString());
            }
        }

        private void FormClick(object? sender, EventArgs e)
        {
            if (game == null || game.GetInputHandler() == null) return;
            if (parentForm != null) parentForm.Focus();
            game.GetInputHandler().Enabled = false;
        }

        private void SurfaceClick(object? sender, EventArgs e)
        {
            if (game == null || game.GetInputHandler() == null) return;
            if (surface != null) surface.Focus();
            game.GetInputHandler().Enabled = true;
        }

        private void WindowKeyReleased(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            KeyReleased(null, e);
        }

        private void WindowClosed(object? sender, FormClosedEventArgs e)
        {
            WindowClose();
        }

        private void WindowKeyPress(object? sender, KeyPressEventArgs e)
        {
            KeyPress(null, e);
        }

        public void SetView(View view)
        {
            if (mainWindow != null)
            {
                mainWindow.SetView(view);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        private void WindowClosedSFML(object? sender, EventArgs e)
        {
            WindowClose();
        }

        private void WindowClose()
        {
            Running = false;
        }

        public void Init(SFML.Graphics.Color background, uint fps = 60)
        {
            Running = true;
            if (mainWindow == null)
            {
                Log.Error("Cannot init. main screen - is null.");
                return;
            }

            mainWindow.SetFramerateLimit(fps);
        }

        public void WindowKeyPressSFML(object? sender, SFML.Window.KeyEventArgs e)
        {
            KeyPress(e, null);
        }

        public void WindowKeyReleasedSFML(object? sender, SFML.Window.KeyEventArgs e)
        {
            KeyReleased(e, null);
        }

        private void KeyPress(SFML.Window.KeyEventArgs ?sfml_e, KeyPressEventArgs ?win_e)
        {
            if (sfml_e != null)
            {
                if (sfml_e.Code == endKey && endKey != Keyboard.Key.Backspace) Running = false;
            }
            else
            {
                
            }
        }
        
        private void KeyReleased(SFML.Window.KeyEventArgs ?sfml_e, System.Windows.Forms.KeyEventArgs ?win_e)
        {
            if (game == null || game.GetInputHandler() == null) return;
            if (sfml_e != null)
            {
                game.GetInputHandler().KeyJustPressed(sfml_e, null);
            }
            else
            {
                game.GetInputHandler().KeyJustPressed(null, win_e);
            }
        }

        public void Update()
        {
            if (Running == false) return;

            if (mainWindow == null)
            {
                Log.Error("Cannot update main screen - is null");
                Running = false;
                throw new NullReferenceException();
            }

            mainWindow.DispatchEvents();
        }

        public void Render()
        {
            if (mainWindow == null) return;

            mainWindow.Clear(SFML.Graphics.Color.Black);

            RenderCurrentScene();

            mainWindow.Display();
        }

        private void RenderCurrentScene()
        {
            if (game == null || mainWindow == null)
            {
                throw new NullReferenceException();
            }

            foreach(Scene pScene in game.GetSceneManager().GetPassiveScenes())
            {
                RenderScenePart(pScene);
            }

            if(game.GetSceneManager() != null)
            {
                if (game.GetSceneManager().CurrentScene != null)
                {
                    RenderScenePart(game.GetSceneManager().CurrentScene);
                }
            }
        }

        public void RenderScenePart(Scene ?scene)
        {
            if (scene == null) return;

            if (mainWindow == null)
            {
                throw new Exception("Main window is null.");
            }

            List<List<Sprite>> layers = scene.GetDrawLayers();

            if (scene.GetTilemap() != null)
            {
                mainWindow.Draw(scene.GetTilemap());
            }

            for (int layer = 0; layer < layers.Count; layer++)
            {
                foreach (Sprite sprite in layers[layer])
                {
                    if (mainWindow == null)
                    {
                        Log.Error("Cannot draw window. Window is null");
                        throw new NullReferenceException();
                    }

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
                            sprite.SetRenderPosition(viewAndPos.Value.X, viewAndPos.Value.Y);
                            mainWindow.Draw(sprite.GetDrawable());
                        }
                    }
                }
            }

            // Render any consols
            if (scene.GetConsole() != null && scene.GetConsole().visible)
            {
                mainWindow.Draw(scene.GetConsole().GetBackground());

                foreach (Text text in scene.GetConsole().GetTexts())
                {
                    mainWindow.Draw(text);
                }

                mainWindow.Draw(scene.GetConsole().GetFPSBackground());
                mainWindow.Draw(scene.GetConsole().GetFPS());
            }
        }

        public XYf PixelToWorld(XYi pos)
        {
            return PixelToWorld(pos.X, pos.Y);
        }
        
        public XYf WorldToPixel(XYf pos)
        {
            return WorldToPixel(pos.X, pos.Y);
        }
        public XYf PixelToWorld(int x, int y)
        {
            if (mainWindow == null)
            {
                throw new NullReferenceException();
            }

            SFML.System.Vector2f world = mainWindow.MapPixelToCoords(new SFML.System.Vector2i(x, y));
            return new XYf(world.X, world.Y);
        }
        public XYf WorldToPixel(float x, float y)
        {
            if (mainWindow == null)
            {
                throw new NullReferenceException();
            }

            SFML.System.Vector2i pixel = mainWindow.MapCoordsToPixel(new SFML.System.Vector2f(x, y));
            return new XYf(pixel.X, pixel.Y);
        }
        public void End()
        {
            Log.Print("Window Closed");
            if (mainWindow != null) mainWindow.Close();
        }
    }
}
