using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    class SelectNearestByRange: Behavior
    {
        UGameObjectBase Target;
        List<UGameObjectBase> targetsList;
        double minimum;
        public SelectNearestByRange(List<UGameObjectBase> l )
        {
            targetsList = l;
        }
        void SetTarget()
        {
            double t, min = 999999999999999999;
            Target = null;
            for (int i = 0; i < targetsList.Count; i++)
            {
                t = (targetsList[i].Par.X - unit.Par.X) * (targetsList[i].Par.X - unit.Par.X) + (targetsList[i].Par.Y - unit.Par.Y) * (targetsList[i].Par.Y - unit.Par.Y);
                if (t < min)
                {
                    min = t;
                    minimum = min;
                    //if(currTarget != null)
                    //game.Map.ContainerSetFrame(currTarget.GetContainerName(), "flyerRed3");
                    Target = targetsList[i];
                    //game.Map.ContainerSetFrame(currTarget.GetContainerName(), "flyerRed2");
                }
            }
        }
        public override void Act()
        { 
        if(Target==null||(Target.Par.X - unit.Par.X) * (Target.Par.X - unit.Par.X) + (Target.Par.Y - unit.Par.Y) * (Target.Par.Y - unit.Par.Y)>minimum)
            {
                SetTarget();
                unit.RemoveBehavior("RotateTo");
                unit.AddBehavior(new RotateTo(Target.Par), "RotateTo");
            }
        
        }
    }
}
