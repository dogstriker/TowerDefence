using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    class SelectNearestByRange: Behavior
    {
        public UGameObjectBase Target { get; private set; }
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
                if (t < min&&t>0)
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
            UGameObjectBase t=Target;
            SetTarget();
            if(t!=Target)
            {
        
                unit.RemoveBehavior("RotateTo");
                unit.AddBehavior(new RotateTo(Target.Par), "RotateTo");
            }
        
        }
    }
}
