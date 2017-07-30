using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;
using MyLib.Utility;
using Dig.Def;

namespace Dig.Stage
{
    class Player : Character
    {
        private InputState input;

        private int hp;
        private bool invincibleStats;
        private int invincibleTime;
        private Sound sound;
        private Motion motion;

        private bool endFlag = false;
        private bool clearFlag = false;

        public Player(Coordinate coordinate, GameDevice gameDevice)
            : base("player", coordinate, 32, 32, gameDevice, 0, 0)
        {
            input = gameDevice.GetInputState();

            hp = 3;
            invincibleStats = false;

            moveTarget = new Coordinate(0, 0);
            moveTarget.SetCoordinate(coordinate);
            direction = Direction.Down;

            motion = new Motion();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    motion.Add(4 * i + j, new Rectangle(32 * j, 32 * i, 32, 32));
                }
            }
            motion.Initialize(new Range(0, 3), new Timer(0.2f));
            sound = gameDevice.GetSound();
        }

        public override void Draw(Renderer renderer)
        {
            if (!invincibleStats)
            {
                renderer.DrawTexture(name, position, motion.DrawinRange());
            }
            else
            {
                if (invincibleTime % 20 > 10)
                {
                    renderer.DrawTexture(name, position, motion.DrawinRange());
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            //移動
            if (isStill)
            {
                position = coordinate.GetVector();
                if (input.Velocity() != Vector2.Zero)
                {
                    velocity = input.Velocity();
                    if (velocity.X > 0)
                    {
                        direction = Direction.Right;
                        motion.SetRange(new Range(8, 11));
                        if (coordinate.column < Screen.MaxColumn - 1) ++moveTarget.column;
                    }
                    else if (velocity.X < 0)
                    {
                        direction = Direction.Left;
                        motion.SetRange(new Range(12, 15));
                        if (coordinate.column > 0) --moveTarget.column;
                    }
                    else if (velocity.Y > 0)
                    {
                        direction = Direction.Down;
                        motion.SetRange(new Range(0, 3));
                        if (coordinate.row < Screen.MaxRow - 1) ++moveTarget.row;
                    }
                    else if (velocity.Y < 0)
                    {
                        direction = Direction.Up;
                        motion.SetRange(new Range(4, 7));
                        if (coordinate.row > 0) --moveTarget.row;
                    }
                    
                    position = position + velocity * speed;

                    isStill = false;
                }
            }
            else
            {
                position = position + velocity * speed;
                if ((position - moveTarget.GetVector()).Length() <= speed)
                {
                    coordinate.SetCoordinate(moveTarget);
                    isStill = true;
                }
            }

            //移動範囲の制限
            var min = new Vector2(0, 32);
            var max = new Vector2(Screen.Width - width, Screen.Height - height);
            position = Vector2.Clamp(position, min, max);

            //攻撃範囲の更新
            UpdateAttackRange();

            //時間になったら次の範囲を選択する（描画処理用）
            motion.Update(gameTime);

            //ダメージ受けたら無敵時間の更新
            if (invincibleStats)
            {
                if (--invincibleTime == 0) invincibleStats = false;
            }
        }
        public void Stop()
        {
            velocity.X = 0.0f;
            velocity.Y = 0.0f;
        }

        public override void Collide(GameObj gameObj)
        {
            switch (gameObj.GetCollideType())
            {
                //通行できない
                case 1:
                    moveTarget.SetCoordinate(coordinate);
                    position = position - velocity * speed;
                    isStill = true;
                    break;
                //罠
                case 2:
                    endFlag = true;
                    Stop();
                    break;
                //ゴール
                case 3:
                    clearFlag = true;
                    endFlag = true;
                    Stop();
                    break;
                //ダメージ受けて、無敵状態になる
                case 4:
                    if (!invincibleStats)
                    {
                        sound.PlaySE("damege");
                        invincibleStats = true;
                        invincibleTime = 120;
                        if (--hp == 0) endFlag = true;
                    }
                    break;
                default:
                    break;
            }
        }

        public override void UpdateAttackRange()
        {
            if (input.GetKeyTrigger(Keys.Space))
            {
                attackRange = GetAttackRange();
                isAttack = true;
            }
        }

        public int GetHP()
        {
            return hp;
        }

        public bool IsEnding()
        {
            return endFlag;
        }

        public bool IsClear()
        {
            return clearFlag;
        }

    }
}
