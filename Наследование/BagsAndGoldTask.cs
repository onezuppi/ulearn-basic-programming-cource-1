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
            if (nextCell != null && nextCell is Sack)
                (deltaX, deltaY) = (0, 0);
            
            return new CreatureCommand
            {
                DeltaX = deltaX,
                DeltaY = deltaY
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack ;
        }
    }

    public class Sack : ICreature
    {
        private int flownСellsCount = 0;

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
                && (Game.Map[x, y + 1] == null || Game.Map[x, y + 1] is Player && flownСellsCount > 0))
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
}