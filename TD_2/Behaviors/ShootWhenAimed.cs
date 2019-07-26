using GameMaps;
using System;
using System.Collections.Generic;

namespace TowerDefence
{
    public class ShootWhenAimed : Behavior
    {
        ITargetProvider tarPr;
        string ShellName;
        List<UGameObjectBase> G;
        double Precision;
        public ShootWhenAimed(ITargetProvider obj, string shellname,List <UGameObjectBase>g, double precision = 3)
        {
            
            tarPr = obj;
            ShellName = shellname;
            G = g;
            Precision = precision;
        }
        public override void Act()
        {
            if (unit.Par.ChargeLevel >= unit.Par.ChargeReady)
            {
                if (tarPr.Target!=null&&Math.Abs(unit.Par.Angle - GameMath.GetAngleOfVector(tarPr.Target.Par.X- unit.Par.X, tarPr.Target.Par.Y- unit.Par.Y)) <= Precision)
                {
                    game.AddShell(ShellName, G, (int)unit.Par.X, (int)unit.Par.Y,(int) unit.Par.Angle,unit.Par.Range);
                    unit.Par.ChargeLevel = 0;
                }


            }
        }
         
    }
}
