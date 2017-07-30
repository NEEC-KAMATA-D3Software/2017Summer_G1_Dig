using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    class Block : GameObj
    {

        public Block(Coordinate coordinate, GameDevice gameDevice)
            : base("block", coordinate, 32, 32, gameDevice, 1, 1)
        { }

        public Block(Block other)
            : this(other.coordinate, other.gameDevice)
        { }

        public override object Clone()
        {
            return new Block(this);
        }

        public override void Collided(GameObj gameObj)
        {

        }
        public override void Attacked(GameObj gameObj)
        {

        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
