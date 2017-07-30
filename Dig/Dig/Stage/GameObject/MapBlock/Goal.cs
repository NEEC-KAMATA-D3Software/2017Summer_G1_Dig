using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    class Goal : GameObj
    {
        int hp;

        public Goal(Coordinate coordinate, GameDevice gameDevice)
            : base("dummy", coordinate, 32, 32, gameDevice, 1, 1)
        {
            hp = 1;
        }

        public Goal(Goal other)
            : this(other.coordinate, other.gameDevice)
        { }

        public override object Clone()
        {
            return new Goal(this);
        }

        public override void Attack(GameObj gameObj)
        {
            if (--hp <= 0)
            {
                name = "goal";
                collideType = 3;
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
