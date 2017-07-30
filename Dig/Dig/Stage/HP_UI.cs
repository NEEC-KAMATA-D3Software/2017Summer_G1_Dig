using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    class HP_UI
    {
        private Player player;
        private int hp;

        public HP_UI(Player player)
        {
            this.player = player;
        }

        public void Draw(Renderer renderer)
        {
            hp = player.GetHP();
            for (int i = 0; i < 3; ++i)
            {
                if (i < hp)
                {
                    renderer.DrawTexture("hp", new Vector2(672 + 32 * i, 0));
                }
                else
                {
                    renderer.DrawTexture("hp0", new Vector2(672 + 32 * i, 0));
                }
            }
        }
    }
}
