using SFML.Graphics;
using Font = SFML.Graphics.Font;

namespace CrossEngine.Engine
{   
    public class ConsoleDisplay
    {
        InputHandler ?inputHandler;
        public bool visible = false;
        private string Name;

        private List<Text> Texts;
        private RectangleShape background;
        private SFML.Graphics.Font defaultFont;
        private int fontSize = 16;

        private int X;
        private int Y;
        private int Width;
        private int Height;
        private int Spacing = 20;

        public ConsoleDisplay(string name, int x, int y, int width, int height)
        {
            Name = name;

            // Load default font if found
            try
            {
                defaultFont = new Font("Assets/fonts/default.ttf");
            }
            catch(Exception ex)
            {
                Log.Error("Default font could not be loaded. Make sure bin has Assets/fonts/default.ttf in it. These files are found in Engine folder.");
                throw new Exception();
            }


            Texts = new List<Text>();
            background = new RectangleShape(new SFML.System.Vector2f(Width, Height));
            background.FillColor = SFML.Graphics.Color.Green;
            background.Position = new SFML.System.Vector2f(X, Y);

            Configure(x, y, width, height);
        }

        public void LinkInputHandler(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }

        public void Configure(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            background.Size = new SFML.System.Vector2f(width, height);
            background.Position = new SFML.System.Vector2f(X, Y);
        }

        public void Print(string text)
        {
            if (Texts.Count >= 5)
            {
                Texts.RemoveAt(0);
            }

            Text newText = new Text(" " + text, defaultFont);
            newText.CharacterSize = (uint)fontSize;
            newText.FillColor = SFML.Graphics.Color.Black;
            newText.Style = Text.Styles.Bold;
            Texts.Add(newText);
        }

        public void Update()
        {
            if (inputHandler != null)
            {
                if (inputHandler.JustPressed != null)
                {
                    if (inputHandler.JustPressed[Keys.Tilde])
                    {
                        visible = !visible;
                    }
                }
            }
            else
            {
                Log.Print("Warn: Console Display: " + Name + " has no input handler.");
            }

                for (int i = 0; i <= 5; i++)
                {
                    if (Texts.Count <= i) break;

                    if (Texts[i] != null)
                    {
                        Texts[i].Position = new SFML.System.Vector2f(X, (Y + (Spacing * i)));
                    }
                    else
                    {
                        break;
                    }
                }
        }

        public List<Text> GetTexts()
        {
            return Texts;
        }

        public RectangleShape GetBackground()
        {
            return background;
        }
    }
}
