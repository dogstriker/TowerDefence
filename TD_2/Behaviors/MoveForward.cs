using System;
using System.Diagnostics;

namespace TowerDefence
{
    public class MoveForward : Behavior
    {
        public MoveForward(/*UGameObjectBase _g, string name*/) /*: base(_g, name)*/
        {
        }

        public override void Act()
        {
            unit.SetCoord(unit.Par.X + unit.Par.Vx, unit.Par.Y + unit.Par.Vy);
        }
    }
}
