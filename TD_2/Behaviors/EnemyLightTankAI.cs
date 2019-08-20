using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaps;
namespace TowerDefence
{
    class EnemyLightTankAI:Behavior
    {
        int currStatus;
        double SquareRadius;
        UGameObjectBase currTarget;
        List<UGameObjectBase> targetList;
        public EnemyLightTankAI(List<UGameObjectBase> l)
        {
            targetList = l;
            
            
        }
        public override void Act()
        {
            double r;
            switch (currStatus)
            { 
                case 1:
                    
                    break;
                case 2:

                    break;
                case 3:
                    r = (currTarget.Par.X - unit.Par.X) * (currTarget.Par.X - unit.Par.X) + (currTarget.Par.Y - unit.Par.Y) * (currTarget.Par.Y - unit.Par.Y);
                    if (r < SquareRadius*0.97)
                    {
                        currStatus = 2;
                        
                        ((UCompositeGameObject)unit).Children[0].AddBehavior(new ShootWhenAimed(currTarget,"LightShell",targetList),"ShotWhenAimed");
                    }
                    break;
            }
        }
        public override void Init(params object[] par)
        {
            SquareRadius = ((UCompositeGameObject)unit).Children[0].Par.Range * ((UCompositeGameObject)unit).Children[0].Par.Range;
            UGameObjectBase tempTarget=null;
            double tempMinAngle = 360;
            double a;
            double r;
            double MinRange = 9999999999;
            //temp out the range target
            UGameObjectBase tempOTRTarget=null;
            
        //поиск цели в радиусе поражения
            for (int i = 0; i < targetList.Count; i++)
            {
                r = (targetList[i].Par.X - unit.Par.X) * (targetList[i].Par.X - unit.Par.X) + (targetList[i].Par.Y - unit.Par.Y) * (targetList[i].Par.Y - unit.Par.Y);
                if (r < SquareRadius)
                {

                    a = GameMath.CompareAngles(unit.Par.Angle, GameMath.GetAngleOfVector(targetList[i].Par.X - unit.Par.X, targetList[i].Par.Y - unit.Par.Y));
                    if (a < tempMinAngle)
                    {
                        tempMinAngle = a;
                        tempTarget = targetList[i];
                        
                       
                    }
                }
                else 
                {
                    if (r < MinRange)
                    {
                        MinRange = r;
                        tempOTRTarget = targetList[i];
                       
                    }
                }
            }
            //поиск цели вне радиуса при необходимости
            unit.RemoveAllBehaviors();
            ((UCompositeGameObject)unit).Children[0].RemoveAllBehaviors();
           
            if (tempTarget != null)
            {
                currStatus = 1;
                currTarget = tempTarget;
            }
            else
            {
                currStatus = 3;
                currTarget = tempOTRTarget;
                unit.AddBehavior(new MoveForward(),"MoveTo");
            }
            unit.AddBehavior(new RotateTo(currTarget), "RotateTo");
            ((UCompositeGameObject)unit).Children[0].AddBehavior(new RotateTo(currTarget), "RotateTo");
        }
    }
}
