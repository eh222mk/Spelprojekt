using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mineclimber.Model
{
    class BatSimulation
    {
        private Bat[] bats;

        public BatSimulation()
        {
            CreateBats();
        }

        internal Bat[] GetBats()
        {
            return bats;
        }

        internal void UpdateBats(float elapsedTime)
        {
            foreach (Bat bat in bats)
            {
                bat.Position += bat.Velocity * elapsedTime;
            }
        }

        private void CreateBats()
        {
            bats = new Bat[4];
            for (int i = 0; i < bats.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        bats[i] = new Bat(new Vector2(1.5f, 1.5f), new Vector2(2, 0)); break;
                    case 1:
                        bats[i] = new Bat(new Vector2(1.5f, 18f), new Vector2(2, 0)); break;
                    case 2:
                        bats[i] = new Bat(new Vector2(9f, 8f), new Vector2(2, 0)); break;
                    case 3:
                        bats[i] = new Bat(new Vector2(1.5f, 57f), new Vector2(2, 0)); break;
                }
            }
        }

    }
}
