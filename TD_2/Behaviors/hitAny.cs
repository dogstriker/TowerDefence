using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    class hitAny:Behavior
    {
         List<UGameObjectBase> targets;
        public hitAny(List<UGameObjectBase> Targets)
        {
            targets = Targets;
        }
        public override void Act()
        {
            for (int i = 0; i < targets.Count; i++)
            {


                if (game.Map.CollisionContainers(unit.Container, targets[i].Container))
                {
                    unit.RemoveBehavior(Name);
                    unit.RemoveBehavior("MoveForward");
                    game.Map.ContainerSetFrame(unit.Container, "exp9");
                    game.Map.AnimationStart(unit.Container, "explosion", 1, remove);
                    unit.SetContainerSize(40, 40);

                }
            }
        }
        void remove()
        {
            unit.removeObject();
        }
    }
}
