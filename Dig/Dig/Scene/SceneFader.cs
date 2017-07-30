using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Utility;
using MyLib.Device;
using Dig.Def;

namespace Dig.Scene
{
    class SceneFader:IScene
    {
        private enum SceneFadeState
        {
            In,
            Out,
            None
        };
        private Timer timer;//フェード時間
        private static float FADE_TIMER = 0.6f;//2秒で
        private SceneFadeState state;
        private IScene scene;//現在のシーン
        private bool isEnd = false;//終了フラグ

        //コンストラクタ
        public SceneFader(IScene scene)
        {
            this.scene = scene;
        }

        //初期化
        public void Initialize()
        {
            scene.Initialize();//現在のシーンを初期化
            state = SceneFadeState.In;//フェードインに設定
            timer = new Timer(FADE_TIMER);
            isEnd = false;
        }

        //更新
        public void Update(GameTime gameTime)
        {
            //状態に応じて更新するシーンを選択
            switch (state)
            {
                case SceneFadeState.In:
                    UpdateFadeIn(gameTime);
                    break;
                case SceneFadeState.Out:
                    UpdateFadeOut(gameTime);
                    break;
                case SceneFadeState.None:
                    UpdateFadeNone(gameTime);
                    break;

            }
        }
        //描画
        public void Draw(Renderer renderer)
        {
            //状態に応じて描画するシーンを選択
            switch (state)
            {
                case SceneFadeState.In:
                    DrawFadeIn(renderer);
                    break;
                case SceneFadeState.Out:
                    DrawFadeOut(renderer);
                    break;
                case SceneFadeState.None:
                    DrawFadeNone(renderer);
                    break;
            }
        }
        //フェードイン状態の更新
        private void UpdateFadeIn(GameTime gameTime)
        {
            //シーンの更新
            scene.Update(gameTime);
            if (scene.IsEnd())
            {
                state = SceneFadeState.Out;
            }
            //時間の更新
            timer.Update();
            if (timer.IsTime())
            {
                state = SceneFadeState.None;
            }
        }
        //フェードアウト状態の更新
        private void UpdateFadeOut(GameTime gameTime)
        {
            //シーンの更新
            scene.Update(gameTime);
            if (scene.IsEnd())
            {
                state = SceneFadeState.Out;
            }
            //時間の更新
            timer.Update();
            if (timer.IsTime())
            {
                isEnd = true;
            }
        }

        //フェードなし状態の更新
        private void UpdateFadeNone(GameTime gameTime)
        {
            scene.Update(gameTime);
            if (scene.IsEnd())
            {
                state = SceneFadeState.Out;
                //フェードアウト用の時間のために初期化
                timer.Initialize();
            }
        }
        //フェードイン状態の描画
        private void DrawFadeIn(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, timer.Rate());
        }
        //フェードアウト状態の描画
        private void DrawFadeOut(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, 1.0f - timer.Rate());
        }
        //フェードイン状態の描画
        private void DrawFadeNone(Renderer renderer)
        {
            scene.Draw(renderer);
        }
        //エフェクト描画
        private void DrawEffect(Renderer renderer, float alpha)
        {
            renderer.Begin();
            renderer.DrawTexture("fade", Vector2.Zero, new Vector2(800, 600), alpha);
            renderer.End();
        }

        //シーンが終了したか？
        public bool IsEnd()
        {
            return isEnd;
        }
        //次のシーンの取得
        public SceneType Next()
        {
            return scene.Next();
        }
        //終了処理
        public void Shutdown()
        {
            scene.Shutdown();
        }
    }
}
