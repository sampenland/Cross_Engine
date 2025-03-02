using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossEngine.Engine.WorldMaker
{
    class WorldWindow : Window
    {
        public WorldWindow(Game game, uint width, uint height, string name, SFML.Window.Keyboard.Key quitKey = SFML.Window.Keyboard.Key.Backspace) : base(game, width, height, name, quitKey)
        {

        }
    }
}
