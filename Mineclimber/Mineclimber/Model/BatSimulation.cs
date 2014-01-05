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
        private CharacterSimulation characterSimulation;

        public BatSimulation(CharacterSimulation cs)
        {
            characterSimulation = cs;
            CreateBats();
        }

        internal Bat[] GetBats()
        {
            return bats;
        }

        internal void UpdateBats(float elapsedTime)
        {
            for (int i = 0; i < bats.Length; i++)
            {
                Vector2 oldPos = bats[i].Position;

                bats[i].Position += bats[i].Velocity * elapsedTime;

                Vector2 newPos = bats[i].Position;
                
                newPos.Y += bats[i].Size.Y;
                newPos.X += bats[i].Size.X / 2;

                Vector2 velocity = bats[i].Velocity;

                if (characterSimulation.didCollide(newPos, bats[i].Size))
                {
                    Vector2 direction = bats[i].Velocity;
                    if (direction.X < 0)
                    {
                        direction.X = bats[i].Speed;
                    }
                    else if(direction.X > 0)
                    {
                        direction.X = -bats[i].Speed;
                    }

                    if (direction.Y < 0)
                    {
                        direction.Y = bats[i].Speed;
                    }
                    else if (direction.Y > 0)
                    {
                        direction.Y = -bats[i].Speed;
                    }
                    bats[i].Velocity = direction;
                }

                bats[i].Position += bats[i].Velocity * elapsedTime;
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
                        bats[i] = new Bat(new Vector2(1.5f, 5f), new Vector2(2, 0)); break;
                    case 1:
                        bats[i] = new Bat(new Vector2(1.5f, 18.5f), new Vector2(2, 0)); break;
                    case 2:
                        bats[i] = new Bat(new Vector2(9f, 8.5f), new Vector2(2, 0)); break;
                    case 3:
                        bats[i] = new Bat(new Vector2(1.5f, 57f), new Vector2(2, 0)); break;
                }
            }
        }

    }
}
