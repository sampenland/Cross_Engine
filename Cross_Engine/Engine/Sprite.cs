using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossEngine.Engine
{
    public class Sprite
    {
        private Game game;
        private Texture ?texture;
        private SFML.Graphics.Sprite ?sprite;
        private Dictionary<View, XYf> views;
        public string ?Name;
        private float _x;
        private float _y;

        public float X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }

        public Sprite(Game game, string textureAsset, int texWidth, int texHeight)
        {
            try
            {
                this.game = game;
                texture = new Texture(textureAsset, new IntRect(0, 0, texWidth, texHeight));
                sprite = new SFML.Graphics.Sprite(texture);
                views = new Dictionary<View, XYf>();
            }
            catch(Exception e)
            {
                Log.Error("Error creating sprite: " + textureAsset);
                texture = null;
                throw new NullReferenceException();
            }
        }

        public void AddToView(View theView)
        {
            if (views.ContainsKey(theView)) return;
            if (game == null || game.gameWindow == null) throw new NullReferenceException();
            game.gameWindow.SetView(theView);
            views.Add(theView, game.gameWindow.WorldToPixel(X, Y));
        }

        public Dictionary<View, XYf> GetViews()
        {
            return views;
        }

        /*
         * World Position
         */
        public void SetWorldPosition(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void SetRenderPosition(float x, float y)
        {
            if (sprite == null)
            {
                return;
            }

            sprite.Position = new SFML.System.Vector2f(x, y);
        }

        public SFML.Graphics.Sprite GetDrawable()
        {
            if (sprite == null) throw new NullReferenceException();
            return sprite;
        }

        public virtual void Update()
        {
            for (int i = 0; i < views.Count; i++)
            {
                View view = views.ElementAt(i).Key;
                views[view].X = X;
                views[view].Y = Y;
            }
        }

    }
}
