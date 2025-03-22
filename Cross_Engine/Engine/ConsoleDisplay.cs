using Cross_Engine.Engine;
using SFML.Graphics;
using Font = SFML.Graphics.Font;

namespace CrossEngine.Engine
{   
    public class ConsoleDisplay
    {
        InputHandler ?inputHandler;
        Game ?game;
        public bool visible = false;
        private string Name;

        private List<Text> Texts;
        private RectangleShape background;
        private int fontSize = 16;
        private SFML.Graphics.Color TextColor;
        private SFML.Graphics.Color BackgroundColor;

        private Text ?FPS;
        private RectangleShape ?FPSBackground;

        private int X;
        private int Y;
        private int Width;
        private int Height;
        private int Spacing = 20;

        public ConsoleDisplay(Game game, string name, int x, int y, int width, int height)
        {
            BackgroundColor = new SFML.Graphics.Color(100, 100, 100);
            TextColor = new SFML.Graphics.Color(255, 255, 255);

            this.game = game;
            Name = name;
            X = x;
            Y = y;

            FPS = new Text("", Common.defaultFont);
            FPS.Position = new SFML.System.Vector2f(10, 2);
            FPS.CharacterSize = (uint)fontSize;
            FPS.FillColor = TextColor;
            FPS.Style = Text.Styles.Bold;

            Texts = new List<Text>();
            background = new RectangleShape(new SFML.System.Vector2f(Width, Height));
            background.FillColor = BackgroundColor;

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

            FPSBackground = new RectangleShape(new SFML.System.Vector2f(Width, 26));
            FPSBackground.FillColor = BackgroundColor;
            FPSBackground.Position = new SFML.System.Vector2f(0, 0);
        }

        public void Print(string text)
        {
            if (Texts.Count >= 5)
            {
                Texts.RemoveAt(0);
            }

            Text newText = new Text(" " + text, Common.defaultFont);
            newText.CharacterSize = (uint)fontSize;
            newText.FillColor = TextColor;
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

        public Text GetFPS()
        {
            if (FPS == null) throw new Exception("FPS error");
            if (game == null) return new Text();
            FPS.DisplayedString = "FPS " + game.FPS.ToString("##.#");
            return FPS;
        }

        public RectangleShape GetFPSBackground()
        {
            if (FPSBackground == null) return new RectangleShape();
            return FPSBackground;
        }
    }
}
