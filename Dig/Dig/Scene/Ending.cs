using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;

namespace Dig.Scene
{
    class Ending : IScene
    {
        private InputState input;
        private bool endFlag;


        public Ending(GameDevice gameDevice)
        {
            input = gameDevice.GetInputState();
            endFlag = false;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("block", new Vector2(64, 0));

            renderer.End();
        }

        public void Initialize()
        {
            endFlag = false;
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public SceneType Next()
        {
            return SceneType.Title;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (input.GetKeyTrigger(Keys.Space))
            {
                endFlag = true;
            }
        }
    }
}
