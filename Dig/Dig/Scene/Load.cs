using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

using Microsoft.Xna.Framework.Input;

namespace Dig.Scene
{
    class Load : IScene
    {
        private Renderer renderer;
        private Sound sound;
        private bool endFlag;
        private TextureLoader textureLoader;
        private BGMLoader bgmLoader;
        private SELoader seLoader;
        private int totalResouceNum;

        private InputState input;

        private string[,] TextureList()
        {
            string path = "./Texture/";
            string[,] list = new string[,]
                {
                    {"black", path},
                    {"player", path},
                    {"hp", path},
                    {"hp0", path},
                    {"goal", path},
                    {"tuti", path},
                    {"hardtuti", path},
                    {"rock", path},
                    {"dummy",path },
                    {"trap", path },
                    {"enemy", path},
                    {"number_red", path },
                    {"gameclear", path },
                    {"gameover", path },
                    {"load", path },
                    {"loadback", path },
                    {"title", path },
                    {"background", path },
                    {"tutorial", path }

                };
            return list;
        }

        //BGMList
             private string[,] BGMList()
        {
            string path = "./BGM/";
            string[,] list = new string[,]
                {
                    {"gameclear",path },
                    {"gameover",path },
                    {"gameplay",path },
                    {"title",path },

                  };
            return list;
        }

        // SEList
        private string[,] SEList()
        {
            string path = "./SE/";
            string[,] list = new string[,]
                {
                    {"attack",path },
                    {"damege",path },

                  };
            return list;
        }


        public Load(GameDevice gameDevice)
        {
            //描画オブジェクトの取得
            renderer = gameDevice.GetRenderer();

            sound = gameDevice.GetSound();

            //画像読み込みオブジェクトの実体生成
            textureLoader = new TextureLoader(renderer, TextureList());

            bgmLoader = new BGMLoader(sound, BGMList());

            seLoader = new SELoader(sound, SEList());

            input = gameDevice.GetInputState();
        }



        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("loadback", Vector2.Zero);

            renderer.DrawTexture("load", new Vector2(200, 450));

            //現在読み込んでいる数を取得
            int currentCount =
                textureLoader.CurrentCount()
            +
            bgmLoader.CurrentCount() +
            seLoader.CurrentCount();

            /*if (totalResouceNum != 0)
            {
                renderer.DrawNumber(
                    "number",
                    new Vector2(20, 100),
                    (int)(currentCount / (float)totalResouceNum * 100));
            }*/

            //終了判定
            if (textureLoader.IsEnd()/* && input.GetKeyTrigger(Keys.Space)*/
            &&
            bgmLoader.IsEnd() &&
            seLoader.IsEnd())
            {
                endFlag = true;
            }

            renderer.End();
        }

        public void Initialize()
        {
            endFlag = false;
            //画像読み込みオブジェクトを初期化
            textureLoader.Initialize();
            bgmLoader.Initialize();
            seLoader.Initialize();

            //全リソース数を計算
            totalResouceNum =
                textureLoader.Count()
            +
            bgmLoader.Count() +
            seLoader.Count();
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
            //画像読み込みが終了してないか？
            if (!textureLoader.IsEnd())
            {
                textureLoader.Update();
            }
            else if (!bgmLoader.IsEnd())
            {
                bgmLoader.Update();
            }
            else if (!seLoader.IsEnd())
            {
                seLoader.Update();
            }
        }
    }
}
