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
                    unit.RemoveBehavior("Hit");
                    unit.RemoveBehavior("MoveForward");
                    unit.SetContainerSize(40, 40);
                    game.Map.AnimationStart(unit.Container, "explosion", 1, remove);

                }
            }
        }
        void remove()
        {
            unit.removeObject();
        }
    }
}
