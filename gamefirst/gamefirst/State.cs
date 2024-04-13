using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamefirst
{
    internal class State
    {
        public List<List<Tile>> level;

        public void LoadLevel(string namefile, Point tileSize)
        {
            var list = new List<int>[1, 1];
            StreamReader sr = new StreamReader(@"..\..\..\levels\" + namefile);
            var line = sr.ReadLine();
            var size = line.Split(' ').Select(x => int.Parse(x)).ToArray();
            level = new List<List<Tile>>();
            for (int i = 0; i < size[1]; i++)
            {
                var lineTiles = line.Split(' ').Select(x => (TileName)int.Parse(x)).ToArray();
                var lineInList = new List<Tile>(); 
                for (int j = 0; j < size[0]; j++)
                     lineInList.Add(new Tile(lineTiles[j], new Rectangle(new Point(i * tileSize.X, j * tileSize.Y), tileSize)));
                level.Add(lineInList);
                line = sr.ReadLine();
            }
            sr.Close();
        }
    }

    internal class Tile
    {
        public readonly TileName Name;
        public readonly Rectangle CollisionRect;
        public Tile(TileName name, Rectangle collisionRect)
        {
            Name = name;
            CollisionRect = collisionRect;
        }
    }

    enum TileName
    {
        Empty,
        Wall       
    }
}
