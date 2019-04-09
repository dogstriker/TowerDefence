using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaps;
namespace TowerDefence
{
    class StopAtPoint:Behavior
    {
        double minimalRange;
        double currentRange;
        int failedAtempts;
        ICoordinateProvider i;
        public override void Act()
        {
            if (Math.Abs(unit.Par.X - i.X) < 4 && Math.Abs(unit.Par.Y - i.Y) < 4)
            {
                unit.RemoveBehavior("MoveForward");
                unit.RemoveBehavior("RotateTo");
            }
            currentRange = (unit.Par.X - i.X) * (unit.Par.X - i.X) + (unit.Par.Y - i.Y) * (unit.Par.Y - i.Y);
            if (minimalRange > currentRange)
            {
                minimalRange = currentRange;
            }
            else 
            {
                failedAtempts++;
            }
            if (failedAtempts > 400)
            {
                unit.RemoveBehavior("MoveForward");
                unit.RemoveBehavior("RotateTo");
            }
        }
        public StopAtPoint(ICoordinateProvider c)
        {
            i = c;
            
        }
        public void SetMinimalRange()
        {
            minimalRange = (unit.Par.X - i.X) * (unit.Par.X - i.X) + (unit.Par.Y - i.Y) * (unit.Par.Y - i.Y);
        }
    }
}
