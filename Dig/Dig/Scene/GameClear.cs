using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;

namespace Dig.Scene
{
    class GameClear : IScene
    {
        private InputState inputState;
        private bool endFlag;
        private Sound sound;

        public GameClear(GameDevice gameDevauce)
        {
            inputState = gameDevauce.GetInputState();
            endFlag = false;
            sound = gameDevauce.GetSound();
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("gameclear", Vector2.Zero);

            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            endFlag = false;
        }

        /// <summary>
        /// タイトルシーン終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return endFlag;
        }

        /// <summary>
        /// タイトルシーンの次のシーン名を取得
        /// </summary>
        /// <returns></returns>
        public SceneType Next()
        {
            sound.StopBGM();
            return SceneType.Title;
        }

        /// <summary>
        /// シーン終了処理
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //スペースキーが押されたらシーン終了
            if (inputState.GetKeyTrigger(Keys.Space))
            {
                endFlag = true;
            }
            sound.PlayBGM("gameclear");
        }
    }
}
