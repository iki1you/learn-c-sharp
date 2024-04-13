using System;
using System.Windows.Forms;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var action = Game.KeyPressed;
            if (action == Keys.Left &&
                x - 1 >= 0 && !(Game.Map[x - 1, y] is Sack))
            {
                return new CreatureCommand() { DeltaX = -1, DeltaY = 0 };
            }
            if (action == Keys.Right &&
                x + 1 < Game.MapWidth && !(Game.Map[x + 1, y] is Sack))
            {
                return new CreatureCommand() { DeltaX = 1, DeltaY = 0 };
            }
            if (action == Keys.Up && y - 1 >= 0
                && !(Game.Map[x, y - 1] is Sack))
            {
                return new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
            }
            if (action == Keys.Down &&
                y + 1 < Game.MapHeight && !(Game.Map[x, y + 1] is Sack))
            {
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
            }
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack)
                return true;
            if (conflictedObject is Monster)
                return true;
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return false;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    class Sack : ICreature
    {
        private int Way;

        public CreatureCommand Act(int x, int y)
        {
            if (y != Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] == null)
                {
                    Way++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
                if (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster)
                {
                    if (Way != 0)
                    {
                        Way++;
                        return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                    }  
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
                }
            }
            if (Way > 1)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            Way = 0;
                

            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    class Monster: ICreature
    {
        private bool playerIsExist;
        private int playerX;
        private int playerY;

        public CreatureCommand Act(int x, int y)
        {
            CheckPlayer();
            if (!playerIsExist)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            var (directionX, directionY) = SetMonsterDirection(x, y);
            var random = new Random();
            var nextTileX = Game.Map[x + directionX, y];
            var nextTileY = Game.Map[x, y + directionY];
            if (nextTileX is Terrain || nextTileX is Sack || nextTileX is Monster)
            {
                if (!(nextTileY is Terrain) && !(nextTileY is Sack) && !(nextTileY is Monster))
                    return new CreatureCommand() { DeltaX = 0, DeltaY = directionY };
            }
            else
            {
                if (!(nextTileY is Terrain) && !(nextTileY is Sack) && !(nextTileY is Monster))
                    if (random.Next(0, 2) == 0)
                        return new CreatureCommand() { DeltaX = directionX, DeltaY = 0 };
                    else
                        return new CreatureCommand() { DeltaX = 0, DeltaY = directionY };
                return new CreatureCommand() { DeltaX = directionX, DeltaY = 0 };
            }
            if (directionX == 0)
                if (!(nextTileY is Terrain) && !(nextTileY is Sack) && !(nextTileY is Monster))
                    return new CreatureCommand() { DeltaX = 0, DeltaY = directionY };
            if (directionY == 0)
                if (!(nextTileX is Terrain) && !(nextTileX is Sack) && !(nextTileX is Monster))
                    return new CreatureCommand() { DeltaX = directionX, DeltaY = 0 };
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        private (int, int) SetMonsterDirection(int x, int y)
        {
            var directionX = 0;
            var directionY = 0;
            if (x > playerX)
                directionX = -1;
            if (x < playerX)
                directionX = 1;
            if (y > playerY)
                directionY = -1;
            if (y < playerY)
                directionY = 1;
            return (directionX, directionY);
        }

        private void CheckPlayer()
        {
            for (var i = 0; i < Game.MapWidth; i++)
                for (var j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j] is Player)
                    {
                        playerIsExist = true;
                        playerX = i;
                        playerY = j;
                    }
                }
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return !(conflictedObject is Gold || conflictedObject is Player);
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
}