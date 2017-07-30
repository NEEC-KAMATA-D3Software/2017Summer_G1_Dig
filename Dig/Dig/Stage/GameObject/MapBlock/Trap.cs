using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    class Trap : GameObj
    {
        int hp;

        public Trap(Coordinate coordinate, GameDevice gameDevice)
            : base("dummy", coordinate, 32, 32, gameDevice, 1, 1)
        {
            hp = 1;
        }

        public Trap(Trap other)
            : this(other.coordinate, other.gameDevice)
        { }

        public override object Clone()
        {
            return new Trap(this);
        }

        public override void Attack(GameObj gameObj)
        {
            if (--hp <= 0)
            {
                name = "trap";
                collideType = 2;
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
