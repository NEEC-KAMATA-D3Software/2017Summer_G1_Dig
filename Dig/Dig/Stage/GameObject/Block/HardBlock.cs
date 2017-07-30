using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage.GameObject.Block
{
    class HardBlock : GameObj
    {
        public HardBlock(Coordinate coordinate, GameDevice gameDevice)
            : base("hardtuti", coordinate, 32, 32, gameDevice, 2, 2)
        { }

        public HardBlock(HardBlock other)
            : this(other.coordinate, other.gameDevice)
        { }

        public override object Clone()
        {
            return new HardBlock(this);
        }

        public override void Collide(GameObj gameObject)
        {
        }

        public override void Attack(GameObj gameObj)
        {

        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
