using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineclimber.Model;
using System.IO;
using Microsoft.Xna.Framework;
namespace Mineclimber.Model
{
    class Level
    {
        /// <summary>
        /// Constants with the level height and width in number of tiles
        /// </summary>
        public const int LevelHeight = 65;
        public const int LevelWidth = 20;

        private const string FileLocationLevel1 = @"..\..\..\..\..\Level.txt";
        private const string FileLocationLevel2 = @"..\..\..\..\..\Level2.txt";
        private const string FileLocationLevel3 = @"..\..\..\..\..\Level3.txt";

        private Tile[][] m_tiles;

        /// <summary>
        /// Constructor
        /// Creates the arrays with tiles and reads the level
        /// </summary>
        public Level()
        {
            m_tiles = new Tile[LevelHeight][];
            ReadLevel(1);
        }

        /// <summary>
        /// Generates the level from the file
        /// </summary>
        internal void ReadLevel(int currentLevel)
        {
            string FileLocation = FindLevelLocation(currentLevel);
            using (StreamReader reader = new StreamReader(FileLocation))
            {
                string line;

                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    m_tiles[counter] = new Tile[line.Length];
                    for (int i = 0; i < line.Length; i++)
                    {
                        string charString = line[i].ToString();
                        if(line[i] == 'X')
                        {
                            m_tiles[counter][i] = new Tile(TileType.Rockblock, new Vector2(i, counter));
                        }
                        else if(line[i] == 'L')
                        {
                            m_tiles[counter][i] = new Tile(TileType.SlideBlockLeft, new Vector2(i, counter), 2);
                        }
                        else if (line[i] == 'R')
                        {
                            m_tiles[counter][i] = new Tile(TileType.SlideBlockRight, new Vector2(i, counter), 2);
                        }
                        else if (line[i] == 'B')
                        {
                            m_tiles[counter][i] = new Tile(TileType.BedRock, new Vector2(i, counter), 20000);
                        }
                        else
                        {
                            m_tiles[counter][i] = new Tile(TileType.Empty, new Vector2(i, counter));
                        }
                    }
                    counter++;
                }
            }//end of using

        }

        private string FindLevelLocation(int currentLevel)
        {
            if (currentLevel == 1)
                return System.IO.Path.GetFullPath(FileLocationLevel1);
            else if (currentLevel == 2)
                return FileLocationLevel2;
            else if (currentLevel == 3)
                return FileLocationLevel3;

            
            throw new ArgumentException("Felaktig level");
        }

        /// <summary>
        /// Returns the tiles of the level
        /// </summary>
        internal Tile[][] Tiles
        {
            get
            {
                return m_tiles;
            }
        }
    }
}
