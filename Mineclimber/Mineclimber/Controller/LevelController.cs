using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineclimber.Model;
using Mineclimber.View;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Mineclimber.Controller
{
    class LevelController
    {
        private Level level;
        private LevelView levelView;
        private Tile[][] levelTiles;
        
        public LevelController(GraphicsDevice graphicsDevice, ContentManager content)
        {
            level = new Level();
            levelTiles = level.Tiles;
            levelView = new LevelView(graphicsDevice, content);
        }

        internal Tile[][] GetTiles()
        {
            return levelTiles;
        }
        
        internal void GenerateNextLevel(int Currentlevel)
        {
            level.ReadLevel(Currentlevel);
            levelTiles = level.Tiles;
        }

        internal void DrawTime(float time, Vector2 position)
        {
            levelView.DrawTimer(time, position);
        }

        internal void DrawLevel(Camera camera)
        {
            foreach(Tile[] tiles in levelTiles)
            {
                foreach(Tile tile in tiles)
                {
                    levelView.DrawLevel(tile, camera);
                }
            }
        }
    }
}
