using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using Dig.Def;

namespace Dig.Stage
{
    class Space : GameObj
    {
        public Space(Coordinate coordinate, GameDevice gameDevice)
            : base("", coordinate, 32, 32, gameDevice, 0, 0)
        { }

        public Space(Space other)
            : this(other.coordinate, other.gameDevice)
        { }

        public override object Clone()
        {
            return new Space(this);
        }

        public override void Draw(Renderer renderer)
        {
        }
    }
}
