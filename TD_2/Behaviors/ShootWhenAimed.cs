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
        public ShootWhenAimed(UGameObjectBase obj, string shellname,List <UGameObjectBase>g)
            
        {
            target = obj;
            ShellName = shellname;
            G = g;
        }
        public override void Act()
        {
            if (unit.Par.ChargeLevel >= unit.Par.ChargeReady)
            {
                if (Math.Abs(unit.Par.Angle - GameMath.GetAngleOfVector(target.Par.X- unit.Par.X, target.Par.X- unit.Par.Y)) <= 2)
                {
                    game.AddShell(ShellName, G, (int)unit.Par.X, (int)unit.Par.Y,(int) unit.Par.Angle);
                    unit.Par.ChargeLevel = 0;
                }


            }
        }
         
    }
}
