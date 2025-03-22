using Cross_Engine.Engine.Interfaces;
using CrossEngine.Engine;
using SFML.Graphics;
using System.Security.Policy;

namespace Cross_Engine.Engine
{
    public class WorldText : GameObject, ITransform
    {
        Text textObject;

        public WorldText(Game game, XYf pos, string text, SFML.Graphics.Font font, int fontSize, SFML.Graphics.Color color) : base(game)
        {
            textObject = new Text(text, font);
            textObject.CharacterSize = (uint)fontSize;
            textObject.FillColor = color;
        }
        public void AddToView(CrossEngine.Engine.View theView)
        {
            if (views.ContainsKey(theView)) return;
            if (game == null || game.gameWindow == null) throw new NullReferenceException();
            game.gameWindow.SetView(theView);
            views.Add(theView, game.gameWindow.WorldToPixel(pos.X, pos.Y));
        }
        public Text GetDrawable()
        {
            if (textObject == null) throw new NullReferenceException();
            return textObject;
        }

        public override void Update()
        {
            base.Update();
            setPos(pos);
        }

        public XYf getPos()
        {
            return new XYf(textObject.Position.X, textObject.Position.Y);
        }

        public void setPos(XYf pos)
        {
            textObject.Position = new SFML.System.Vector2f(pos.X, pos.Y);
        }
    }
}
