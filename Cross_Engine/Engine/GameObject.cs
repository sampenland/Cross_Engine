using CrossEngine.Engine;
using SFML.Graphics;
using System.Security.Policy;
namespace Cross_Engine.Engine
{
    public enum DRAW_TYPES
    {
        NULL,
        Sprite,
        WorldText,
    }

    public abstract class GameObject
    {
        public DRAW_TYPES drawType = DRAW_TYPES.NULL;
        public XYf pos;
        public string Name;
        protected Dictionary<CrossEngine.Engine.View, XYf> views;
        protected Game game;

        public GameObject(Game game)
        {
            pos = new XYf(0, 0);
            views = new Dictionary<CrossEngine.Engine.View, XYf>();
            this.game = game;
            Name = "";
        }

        public Dictionary<CrossEngine.Engine.View, XYf> GetViews()
        {
            return views;
        }

        /*
         * World Position
         */
        public void SetWorldPosition(float x, float y)
        {
            pos.X = x;
            pos.Y = y;
        }

        public virtual void SetRenderPosition(float x, float y) { }

        public virtual void Update() { }
    }
}
