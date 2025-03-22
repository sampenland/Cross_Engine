using Cross_Engine.Engine;
using CrossEngine.Engine;

namespace Cross_Engine.Examples
{
    class DrawableExamples : Scene
    {
        public DrawableExamples(Game game, string name) : base(game, name)
        {

        }

        public override void Start()
        {
            if (game != null && game.gameWindow != null)
            {

                if (Common.defaultFont != null)
                {
                    CrossEngine.Engine.View mainView = new CrossEngine.Engine.View(0, 0, game.gameWindow.Width, game.gameWindow.Height);
                    AddView(mainView);
                    WorldText text = new WorldText(game, new XYf(100, 100), "Example Text", Common.defaultFont, 20, SFML.Graphics.Color.Blue);
                    text.AddToView(mainView);
                }
            }
        }
    }
}
