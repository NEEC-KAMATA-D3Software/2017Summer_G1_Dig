using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    class Block : GameObj
    {
        int hp;
        private Sound sound;

        public Block(Coordinate coordinate, GameDevice gameDevice, string name, int hp)
            : base(name, coordinate, 32, 32, gameDevice, 1, 1)
        {
            this.hp = hp;
            sound = gameDevice.GetSound();
        }

        public Block(Block other)
            : this(other.coordinate, other.gameDevice, other.name, other.hp)
        { }

        public override object Clone()
        {
            return new Block(this);
        }

        public override void Attack(GameObj gameObj)
        {
            sound.PlaySE("attack");
            if (--hp == 0) isDead = true;
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
