using System.Diagnostics;
using GameMaps;
using System;
namespace TowerDefence
{
    public class MoveForward : Behavior
    {
        public MoveForward(UGameObjectBase g) : base(g)
        {
        }

        public override void Act()
        {
            Debug.WriteLine("MoveForward");
            unit.SetCoord(unit.Par.X + unit.Par.Vx, unit.Par.Y + unit.Par.Vy);
        }
    }
    public class ShootWhenAimed : Behavior
    {
        double Tx;
        double Ty;
        public ShootWhenAimed(UGameObjectBase g,double tx, double ty): base(g)
            
        {
            Tx = tx;
            Ty = ty;
        }
        public override void Act()
        {
            if (unit.Par.ChargeLevel == unit.Par.ChargeReady)
            {
                if (Math.Abs(unit.Par.Angle - GameMath.GetAngleOfVector(Tx- unit.Par.X, Ty- unit.Par.Y)) <= 2)
                {
                    game.AddObject("Rocket", unit.Par);
                }


            }
        }
         
    }
}
