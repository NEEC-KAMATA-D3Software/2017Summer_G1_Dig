using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using Dig.Stage;
using System;

namespace Dig.Scene
{
    class GamePlay : IScene
    {
        private GameDevice gameDevice;
        private InputState input;

        private bool endFlag;
        private bool clearFlag;

        private Map map;

        private CharacterManager characterManager;
        private Player player;

        private Timer timer;
        private HP_UI hp_UI;
        Random rand = new Random();
        private Sound sound;
        

        public GamePlay(GameDevice gameDevice)
        {
            this.gameDevice = gameDevice;
            input = gameDevice.GetInputState();
            endFlag = false;

            timer = new Timer(61);
            sound = gameDevice.GetSound();
            

            characterManager = new CharacterManager(gameDevice);
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("background", Vector2.Zero);

            //stage
            map.Draw(renderer);
            characterManager.Draw(renderer);

            //UI
            for (int i = 0; i < 24; ++i)
            {
                renderer.DrawTexture("black", new Vector2(i * 32, 0));
            }
            int time = (int)timer.Now() / 60;
            if (time > 10)
            {
                renderer.DrawNumber("number", Vector2.Zero, time);
            }
            else if(time<=10)
            {
                renderer.DrawNumber("number_red", Vector2.Zero, time);
            }
            hp_UI.Draw(renderer);
            //if(time>59)
            //{
            //    renderer.DrawTexture("game_over", new Vector2(180, 200));
            //}

            renderer.End();
        }

        public void Initialize()
        {
            endFlag = false;

            timer.Initialize();

            characterManager.Initialize();

            //マップ
            map = new Map(gameDevice);
            //int a = rand.Next(0, 2);
            //if (a == 0)
            //{
                map.Load("stage.csv");
            //}
            //else if (a == 1)
            //{
            //    map.Load("stage1.csv");
            //}

            //ゴールを設定する
            bool goalSet = false;
            bool trapSet = false;
            for(int i = 1;i <= 3; i++)
            {
                    trapSet = false;
                while (!trapSet)
                {
                    Coordinate coordinate = new Coordinate(rand.Next(5, 16), rand.Next(6, 24));
                    if (map.Changeable(coordinate))
                    {
                        map.ChangeObject(new Trap(coordinate, gameDevice), coordinate);
                        trapSet = true;
                    }
                }
            }
            while (!goalSet)
            {
                Coordinate coordinate = new Coordinate(rand.Next(5, 16), rand.Next(6, 24));
                if (map.Changeable(coordinate))
                {
                    map.ChangeObject(new Goal(coordinate, gameDevice), coordinate);
                    goalSet = true;
                }
            }

            characterManager.Add(map);

            //初期キャラ
            player = new Player(new Coordinate(0, 0), gameDevice);
            characterManager.Add(player);
            characterManager.Add(new Enemy(new Coordinate(3, 6), gameDevice, map, player));
            characterManager.Add(new Enemy(new Coordinate(11, 21), gameDevice, map, player));
            characterManager.Add(new Enemy(new Coordinate(14, 11), gameDevice, map, player));

            hp_UI = new HP_UI(player);
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public SceneType Next()
        {
            
            sound.StopBGM();
            if (clearFlag)
            {
                //player.GetPosition()
                return SceneType.GameClear;
            }
            return SceneType.GameOver;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            timer.Update();

            if (player.IsEnding() || timer.IsTime())
            {
                clearFlag = player.IsClear();
                endFlag = true;
            }
            sound.PlayBGM("gameplay");
            characterManager.Update(gameTime);
            map.Update(gameTime);
        }
    }
}
