using SFML.Graphics;
using SFML.System;

namespace CrossEngine.Engine
{
    public class TileMap : Drawable
    {
        Game game;
        public Texture Tileset;
        private VertexArray vertices = new VertexArray();
        private List<View> views;

        public TileMap(Game game, string asset)
        {
            this.game = game;
            Tileset = new Texture(asset);
            views = new List<View>();
        }

        public void AddToView(View view)
        {
            if (views.Contains(view)) return;
            views.Add(view);
        }

        public List<View> GetViews()
        {
            return views;
        }

        public bool Load(XYi tileSize, int[] tiles, int width, int height)
        {

            // resize the vertex array to fit the level size
            vertices.PrimitiveType = SFML.Graphics.PrimitiveType.Quads;
            vertices.Resize((uint)width * (uint)height * 4);

            // populate the vertex array, with one quad per tile
            for (uint i = 0; i < width; ++i)
            {
                for (uint j = 0; j < height; ++j)
                {
                    // get the current tile number
                    int tileNumber = tiles[i + j * width];

                    // find its position in the tileset texture
                    long tu = tileNumber % (Tileset.Size.X / tileSize.X);
                    long tv = tileNumber / (Tileset.Size.X / tileSize.X);

                    // get a pointer to the current tile's quad
                    uint index = (i + j * (uint)width) * 4;

                    // define its 4 corners
                    vertices[index + 0] = new Vertex(new Vector2f(i * tileSize.X, j * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                    vertices[index + 1] = new Vertex(new Vector2f((i + 1) * tileSize.X, j * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                    vertices[index + 2] = new Vertex(new Vector2f((i + 1) * tileSize.X, (j + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                    vertices[index + 3] = new Vertex(new Vector2f(i * tileSize.X, (j + 1) * tileSize.Y), new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y));
                }
            }

            return true;
        }

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            
            states.Texture = Tileset;
            
            if(game == null || game.gameWindow == null)
            {
                Log.Error("Error drawing Tilemap. View may not be set.");
                throw new NullReferenceException();
            }

            foreach(View view in views)
            {
                if (view == null) continue;
                game.gameWindow.SetView(view);
                target.Draw(vertices, states);
            }
        }

    }
}