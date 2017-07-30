using Microsoft.Xna.Framework;
using Dig.Def;
using MyLib.Device;
using MyLib.Utility;

namespace Dig.Stage
{
    class Enemy : Character
    {
        private Map map;
        private Player player;

        private bool oneMove;

        private bool wait;
        private Timer waitTimer;
        private Sound sound;

        private bool attack;
        private Timer attackTimer;

        private bool stunStats;
        private Timer stunTimer;

        public Enemy(Coordinate coordinate, GameDevice gameDevice, Map map, Player player)
            : base("black", coordinate, 32, 32, gameDevice, 4, 0)
        {
            this.map = map;
            this.player = player;

            moveTarget = new Coordinate(0, 0);
            moveTarget.SetCoordinate(coordinate);
            direction = Direction.Down;

            oneMove = false;
            wait = false;
            waitTimer = new Timer(0.5f);
            attack = false;
            attackTimer = new Timer(5.0f);
            stunStats = false;
            stunTimer = new Timer(3.0f);
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture("enemy", position, new Rectangle(0, 0, 32, 32));
        }

        public override void Update(GameTime gameTime)
        {
            attackTimer.Update();
            if (wait == false && attack==false && stunStats == false)
            {
                if (coordinate.GetVector() != moveTarget.GetVector())
                {
                    position = position + velocity * speed;

                    if ((moveTarget.GetVector() - position).Length() <= speed)
                    {
                        coordinate.SetCoordinate(moveTarget);
                        position = coordinate.GetVector();
                        velocity = Vector2.Zero;
                        wait = true;
                        waitTimer.Initialize();
                        oneMove = false;
                    }
                }
                else
                {   
                    TryMove(PlayerDirection());
                }
            }
            else if (wait == true)
            {
                waitTimer.Update();
                if (waitTimer.IsTime()) wait = false;
            }
            //else if (attack == true)
            //{
            //    attackTimer.Update();
            //    if (attackTimer.IsTime()) attack = false;
            //}
            else
            {
                stunTimer.Update();
                if (stunTimer.IsTime()) stunStats = false;
            }
        }

        private Direction PlayerDirection()
        {
            if (player.GetCoordinate().row > coordinate.row) return Direction.Down;
            else if (player.GetCoordinate().row < coordinate.row) return Direction.Up;
            else if (player.GetCoordinate().column > coordinate.column) return Direction.Right;
            else return Direction.Left;
        }

        private void TryMove(Direction direction)
        {
            oneMove = true;
            switch (direction)
            {
                case Direction.Down:
                    if (coordinate.row < Screen.MaxRow - 1)
                    {
                        ++moveTarget.row;
                        velocity.Y = 1.0f;
                    }
                    break;
                case Direction.Up:
                    if (coordinate.row > 0)
                    {
                        --moveTarget.row;
                        velocity.Y = -1.0f;
                    }
                    break;
                case Direction.Left:
                    if (coordinate.column > 0)
                    {
                        --moveTarget.column;
                        velocity.X = -1.0f;
                    }
                    break;
                case Direction.Right:
                    if (coordinate.column < Screen.MaxColumn - 1)
                    {
                        ++moveTarget.column;
                        velocity.X = 1.0f;
                    }
                    break;
                default:
                    break;
            }
            //移動できないと破壊する
            if (!map.Movable(moveTarget))
            {
                if (attackTimer.IsTime())
                {
                    map.AttackTo(this, moveTarget);
                    attackTimer.Initialize();
                }
                moveTarget.SetCoordinate(coordinate);
                velocity = Vector2.Zero;
                oneMove = false;
            }
        }

        public override void Attack(GameObj gameObj)
        {
            stunStats = true;
            stunTimer.Initialize();
        }

    }
}
