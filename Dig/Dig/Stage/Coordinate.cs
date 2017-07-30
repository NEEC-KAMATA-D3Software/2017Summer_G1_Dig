using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Dig.Stage
{
    class Coordinate
    {

        public int row;
        public int column;

        public Coordinate(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public void SetCoordinate(Coordinate coordinate)
        {
            row = coordinate.row;
            column = coordinate.column;
        }

        public Vector2 GetVector()
        {
            return new Vector2(column * 32, row * 32 + 32);
        }

    }
}
