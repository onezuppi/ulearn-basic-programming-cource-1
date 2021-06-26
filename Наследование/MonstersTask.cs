using System;
using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            var (deltaX, deltaY) = (0, 0);
            if (Game.KeyPressed == Keys.Left && x - 1 >= 0)
                deltaX = -1;
            else if (Game.KeyPressed == Keys.Right && x + 1 < Game.MapWidth)
                deltaX = 1;
            else if (Game.KeyPressed == Keys.Up && y - 1 >= 0)
                deltaY = -1;
            else if (Game.KeyPressed == Keys.Down && y + 1 < Game.MapHeight)
                deltaY = 1;

            var nextCell = Game.Map[x + deltaX, y + deltaY];
            if (nextCell is Sack)
                (deltaX, deltaY) = (0, 0);

            return new CreatureCommand
            {
                DeltaX = deltaX,
                DeltaY = deltaY
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }
    }

    public class Sack : ICreature
    {
        private int flownСellsCount;

        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public CreatureCommand Act(int x, int y)
        {
            if (y + 1 < Game.MapHeight
                && (Game.Map[x, y + 1] == null || (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster) 
                    && flownСellsCount > 0))
            {
                flownСellsCount++;
                return new CreatureCommand {DeltaY = 1};
            }

            if (flownСellsCount > 1)
                return new CreatureCommand {TransformTo = new Gold()};
            flownСellsCount = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
                Game.Scores += 10;
            return true;
        }
    }

    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            var (pX, pY) = GetPlayerPosition();
            if (pX == -1 || pY == -1)
                return command;
            var currentDistance = CalculateDistance(pX, pY, x, y);
            if (x + 1 < Game.MapWidth && IsNotDangerCell(Game.Map[x + 1, y])
                                      && currentDistance > CalculateDistance(pX, pY, x + 1, y))
                command.DeltaX = 1;
            else if (x - 1 >= 0 && IsNotDangerCell(Game.Map[x - 1, y])
                                && currentDistance > CalculateDistance(pX, pY, x - 1, y))
                command.DeltaX = -1;
            else if (y + 1 < Game.MapHeight && IsNotDangerCell(Game.Map[x, y + 1])
                                            && currentDistance > CalculateDistance(pX, pY, x, y + 1))
                command.DeltaY = 1;
            else if (y - 1 >= 0 && IsNotDangerCell(Game.Map[x, y - 1])
                                && currentDistance > CalculateDistance(pX, pY, x, y - 1))
                command.DeltaY = -1;
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        private static (int x, int y) GetPlayerPosition()
        {
            for (var x = 0; x < Game.MapWidth; x++)
            {
                for (var y = 0; y < Game.MapHeight; y++)
                {
                    if (Game.Map[x, y] != null && Game.Map[x, y] is Player)
                        return (x, y);
                }
            }

            return (-1, -1);
        }

        private static double CalculateDistance(int x1, int y1, int x2, int y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private static bool IsNotDangerCell(ICreature cell)
        {
            return cell == null || cell is Player || cell is Gold;
        }
    }
}