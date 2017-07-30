using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;
using MyLib.Utility;

namespace Dig.Scene
{
    class Operation:IScene
    {
        private InputState inputState;
        private bool endFlag;
        private Timer timer;


        public Operation(GameDevice gameDevauce)
        {
            inputState = gameDevauce.GetInputState();
            endFlag = false;
            timer = new Timer(3.7f);
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("background", Vector2.Zero);
            renderer.DrawTexture("tutorial", Vector2.Zero);
            renderer.DrawTexture("hardtuti", new Vector2(510, 440));

            renderer.End();
        }
        public bool IsEnd()
        {
            return endFlag;
        }
        public void Initialize()
        {
            endFlag = false;
            timer.Initialize();
        }

        public SceneType Next()
        {
            return SceneType.GamePlay;
        }
        public void Shutdown()
        {
        }
        public void Update(GameTime gameTime)
        {
            timer.Update();
            //スペースキーが押されたらシーン終了
            if (timer.Now()==0|| inputState.GetKeyTrigger(Keys.Space))
            {
                endFlag = true;
            }
        }
    }
}
