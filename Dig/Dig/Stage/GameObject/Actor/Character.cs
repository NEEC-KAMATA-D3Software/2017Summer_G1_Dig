using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    abstract class Character : GameObj
    {

        //移動用
        protected bool isStill = true;
        protected Coordinate moveTarget;
        protected Vector2 velocity;
        protected float speed = 3.0f;

        //攻撃用
        protected bool isAttack = false;
        protected Direction direction = Direction.Down;
        protected Rectangle attackRange;

        public Character(string name, Coordinate coordinate, int height, int width, GameDevice gameDevice, int collideType, int attackType)
            : base(name, coordinate, height, width, gameDevice, collideType, attackType)
        { }

        public bool IsAttack()
        {
            return isAttack;
        }

        public void AttackEnd()
        {
            isAttack = false;
        }

        public Rectangle GetAttackRange()
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Rectangle((int)position.X, (int)position.Y + 32, 32, 32);
                case Direction.Up:
                    return new Rectangle((int)position.X, (int)position.Y - 32, 32, 32);
                case Direction.Right:
                    return new Rectangle((int)position.X + 32, (int)position.Y, 32, 32);
                case Direction.Left:
                    return new Rectangle((int)position.X - 32, (int)position.Y, 32, 32);
                default:
                    return new Rectangle(-1, -1, 0, 0);
            }
        }

        public bool IsAttackRange(GameObj other)
        {
            return GetAttackRange().Intersects(other.GetRectangle());
        }

        public int GetAttackType()
        {
            return attackType;
        }

        public virtual void UpdateAttackRange() { }

    }
}
