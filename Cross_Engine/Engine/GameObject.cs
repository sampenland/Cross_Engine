using CrossEngine.Engine;
namespace Cross_Engine.Engine
{
    public abstract class GameObject
    {
        public XYf pos;
        protected Dictionary<CrossEngine.Engine.View, XYf> views;
        protected Game game;

        public GameObject(Game game)
        {
            pos = new XYf(0, 0);
            views = new Dictionary<CrossEngine.Engine.View, XYf>();
            this.game = game;
        }

        public virtual void Update() { }
    }
}
