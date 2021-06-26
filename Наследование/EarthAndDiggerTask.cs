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
            return 1;
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

            return new CreatureCommand
            {
                DeltaX = deltaX,
                DeltaY = deltaY
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }
}