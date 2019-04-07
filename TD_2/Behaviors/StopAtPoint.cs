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
        ICoordinateProvider i;
        public override void Act()
        {
            if (Math.Abs(unit.Par.X - i.X) < 4 && Math.Abs(unit.Par.Y - i.Y) < 4)
            {
                unit.RemoveBehavior("MoveForward");
                unit.RemoveBehavior("RotateTo");
            }
        }
        public StopAtPoint(ICoordinateProvider c)
        {
            i = c;

        }
    }
}
