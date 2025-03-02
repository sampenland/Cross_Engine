using SFML.Graphics;

namespace CrossEngine.Engine
{
    public class View : SFML.Graphics.View
    {
        private Sprite ?centerOn;
        public View(int x, int y, int width, int height) : base(new SFML.Graphics.FloatRect(x, y, width, height))
        {
           
        }

        public View(View viewToAnotherView, XYf viewPortPosition, XYf viewPortSize) : base(viewToAnotherView)
        {
            Viewport = new SFML.Graphics.FloatRect(new SFML.System.Vector2f(viewPortPosition.X, viewPortPosition.Y),
                new SFML.System.Vector2f(viewPortSize.X, viewPortSize.Y));
        }

        public void CenterOn(Sprite sprite)
        {
            centerOn = sprite;
        }

        public void Update()
        {
            if (centerOn != null)
            {
                XYf pos = centerOn.GetViews()[this];
                Center = new SFML.System.Vector2f(pos.X, pos.Y);
            }
        }
    }
}
