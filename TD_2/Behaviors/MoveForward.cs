using System;
using System.Diagnostics;

namespace TowerDefence
{
    public class MoveForward : Behavior
    {
        private int limXmin, limXmax, limYmin, limYmax;
        /// <summary>
        /// При выходе за границу карты более, чем на XAbsolute или YAbsolute, умножить на limitMultiplier, объект уничтожается
        /// </summary>
        /// <param name="limitMultiplier"></param>
        public MoveForward(int limitMultiplier = 1) /*: base(_g, name)*/
        {
            limXmin = -game.Map.XAbsolute * limitMultiplier;
            limXmax = game.Map.XAbsolute * (limitMultiplier + 1);
            limYmin = -game.Map.YAbsolute * limitMultiplier;
            limYmax = game.Map.YAbsolute * (limitMultiplier + 1);
        }

        public override void Act()
        {
            if (unit.Par.Par.ContainsKey("Distance"))
            {
                unit.Par.Par["Distance"] += unit.Par.Velocity;
                if (unit.Par.Par["Distance"] >= unit.Par.Range)
                {
                    unit.RemoveAllBehaviors();
                    unit.removeObject();
                }
            }
            
            unit.SetCoord(unit.Par.X + unit.Par.Vx, unit.Par.Y + unit.Par.Vy);
            if(unit.Par.X < limXmin || unit.Par.X > limXmax || unit.Par.Y < limYmin || unit.Par.Y > limYmax)
            {
                unit.RemoveAllBehaviors();
                unit.removeObject();
            }
        }
    }
}
