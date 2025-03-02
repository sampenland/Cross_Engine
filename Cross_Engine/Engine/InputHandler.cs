using SFML.Window;
using System.Reflection.Metadata.Ecma335;

namespace CrossEngine.Engine
{
    public enum Keys
    {
        LeftArrow,
        RightArrow,
        UpArrow,
        DownArrow,

        W,
        A,
        S,
        D,

        Tilde,
    }

    public class InputHandler
    {
        public Dictionary<Keys, bool> ?PressedKeys;
        public Dictionary<Keys, bool> ?JustPressed;
        public bool Enabled = true;

        public InputHandler()
        {
            PressedKeys = new Dictionary<Keys, bool>();
            JustPressed = new Dictionary<Keys, bool>();

            PopulatePressedKeys();
            PopulateJustPressed();

        }

        private void PopulatePressedKeys()
        {
            if (PressedKeys == null) return;

            PressedKeys.Add(Keys.LeftArrow, false);
            PressedKeys.Add(Keys.RightArrow, false);
            PressedKeys.Add(Keys.UpArrow, false);
            PressedKeys.Add(Keys.DownArrow, false);

            PressedKeys.Add(Keys.W, false);
            PressedKeys.Add(Keys.A, false);
            PressedKeys.Add(Keys.S, false);
            PressedKeys.Add(Keys.D, false);
        }

        public void PopulateJustPressed()
        {
            if (JustPressed == null) return;

            JustPressed.Add(Keys.Tilde, false);
        }

        public void ClearJustPressed()
        {
            if (JustPressed == null)
            {
                throw new Exception("Input handler not created properly");
            }

            JustPressed.Clear();
            PopulateJustPressed();
        }

        public void KeyJustPressed(SFML.Window.KeyEventArgs ?sfml_e, System.Windows.Forms.KeyEventArgs ?win_e)
        {
            if (!Enabled) return;

            if (JustPressed == null) return;

            if (sfml_e != null)
            {
                if (sfml_e.Code == Keyboard.Key.Grave)
                {
                    JustPressed[Keys.Tilde] = true;
                }
            }
            else if(win_e != null)
            {
                if (win_e.KeyCode == System.Windows.Forms.Keys.Oemtilde)
                {
                    JustPressed[Keys.Tilde] = true;
                }
            }
        }

        public void Update()
        {
            if (!Enabled) return;

            if (PressedKeys == null) return;

            PressedKeys[Keys.UpArrow] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Up);
            PressedKeys[Keys.DownArrow] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Down);
            PressedKeys[Keys.RightArrow] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Right);
            PressedKeys[Keys.LeftArrow] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Left);

            PressedKeys[Keys.W] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.W);
            PressedKeys[Keys.A] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.A);
            PressedKeys[Keys.S] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.S);
            PressedKeys[Keys.D] = SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.D);

        }

    }
}
