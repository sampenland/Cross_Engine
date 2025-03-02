using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossEngine.Engine
{
    abstract class ArrowActor : Sprite
    {
        InputHandler ?inputHandler;
        private float speed = 1f;
        private bool wasd = true;
        private bool movementEnabled = true;

        protected ArrowActor(Game game, float movementSpeed, string textureAsset, int texWidth, int texHeight) : 
            base(game, textureAsset, texWidth, texHeight)
        {
            speed = movementSpeed;
        }

        public void LinkinputHandler(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }

        public void EnableMovement()
        {
            movementEnabled = true;
        }

        public void DisableMovement()
        {
            movementEnabled = false;
        }

        public void DisableWASD()
        {
            wasd = false;
        }

        public void EnableWASD()
        {
            wasd = true;
        }

        public override void Update()
        {
            if (inputHandler != null)
            {
                if (inputHandler.PressedKeys != null)
                {
                    if (inputHandler.PressedKeys[Keys.UpArrow] || wasd && inputHandler.PressedKeys[Keys.W])
                    {
                        Y -= speed;
                    }
                }

                if (inputHandler.PressedKeys != null)
                {
                    if (inputHandler.PressedKeys[Keys.DownArrow] || wasd && inputHandler.PressedKeys[Keys.S])
                    {
                        Y += speed;
                    }
                }

                if (inputHandler.PressedKeys != null)
                {
                    if (inputHandler.PressedKeys[Keys.RightArrow] || wasd && inputHandler.PressedKeys[Keys.D])
                    {
                        X += speed;
                    }
                }

                if (inputHandler.PressedKeys != null)
                {
                    if (inputHandler.PressedKeys[Keys.LeftArrow] || wasd && inputHandler.PressedKeys[Keys.A])
                    {
                        X -= speed;
                    }
                }
            }
            
            base.Update();
        }
    }
}
