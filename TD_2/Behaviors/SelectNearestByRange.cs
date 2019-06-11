using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Behaviors
{
    class SelectNearestByRange: Behavior
    {
        UGameObjectBase Target;
        List<UGameObjectBase> targetsList;
        public SelectNearestByRange(List<UGameObjectBase> l )
        {
            targetsList = l;
        }
        void SetTarget()
        {
            double t, min = 361;
            Target = null;
            for (int i = 0; i < targetsList.Count; i++)
            {
                
                if (t < min)
                {
                    min = t;
                    //if(currTarget != null)
                    //game.Map.ContainerSetFrame(currTarget.GetContainerName(), "flyerRed3");
                    Target = targetsList[i];
                    //game.Map.ContainerSetFrame(currTarget.GetContainerName(), "flyerRed2");
                }
            }
        }
    }
}
