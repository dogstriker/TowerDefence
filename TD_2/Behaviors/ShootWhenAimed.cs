using GameMaps;
using System;
using System.Collections.Generic;
namespace TowerDefence
{
    public class ShootWhenAimed : Behavior
    {
        UGameObjectBase target;
        string ShellName;
        List<UGameObjectBase> G;
        double Precision;
        public ShootWhenAimed(UGameObjectBase obj, string shellname,List <UGameObjectBase>g, double precision = 3)
        {
            target = obj;
            ShellName = shellname;
            G = g;
            Precision = precision;
        }
        public override void Act()
        {
            if (unit.Par.ChargeLevel >= unit.Par.ChargeReady)
            {
                if (Math.Abs(unit.Par.Angle - GameMath.GetAngleOfVector(target.Par.X- unit.Par.X, target.Par.Y- unit.Par.Y)) <= Precision)
                {
                    game.AddShell(ShellName, G, (int)unit.Par.X, (int)unit.Par.Y,(int) unit.Par.Angle);
                    unit.Par.ChargeLevel = 0;
                }


            }
        }
         
    }
}
