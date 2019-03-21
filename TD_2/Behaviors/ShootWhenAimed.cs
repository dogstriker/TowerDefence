using GameMaps;
using System;
namespace TowerDefence
{
    public class ShootWhenAimed : Behavior
    {
        double Tx;
        double Ty;
        public ShootWhenAimed(double tx, double ty)
            
        {
            Tx = tx;
            Ty = ty;
        }
        public override void Act()
        {
            if (unit.Par.ChargeLevel >= unit.Par.ChargeReady)
            {
                if (Math.Abs(unit.Par.Angle - GameMath.GetAngleOfVector(Tx- unit.Par.X, Ty- unit.Par.Y)) <= 2)
                {
                    game.AddObject("Rocket", unit.Par);
                    unit.Par.ChargeLevel = 0;
                }


            }
        }
         
    }
}
