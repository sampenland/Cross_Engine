﻿using Cross_Engine.Engine;
using SFML.Graphics;

namespace CrossEngine.Engine
{
    public class Sprite : GameObject
    {
        private Texture ?texture;
        private SFML.Graphics.Sprite ?sprite;
        public string ?Name;

        public float X
        {
            get
            {
                return pos.X;
            }

            set
            {
                pos.X = value;
            }
        }

        public float Y
        {
            get
            {
                return pos.Y;
            }

            set
            {
                pos.Y = value;
            }
        }

        public Sprite(Game game, string textureAsset, int texWidth, int texHeight) : base(game)
        {
            try
            {
                texture = new Texture(textureAsset, new IntRect(0, 0, texWidth, texHeight));
                sprite = new SFML.Graphics.Sprite(texture);
                views = new Dictionary<View, XYf>();
                drawType = DRAW_TYPES.Sprite;
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

        public override void SetRenderPosition(float x, float y)
        {
            base.SetRenderPosition(x, y);

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

        public override void Update()
        {
            base.Update();

            if (views != null)
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
}
