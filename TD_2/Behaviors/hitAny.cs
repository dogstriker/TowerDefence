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
                   
                    targets[i].Par.HP -= unit.Par.DamageMax;
                    if (targets[i].Par.HP > 0)
                    {
                        game.Map.ContainerSetFrame(unit.Container, "exp9");
                        game.Map.AnimationStart(unit.Container, "explosion", 1, remove);
                        unit.SetContainerSize(40, 40);


                    }
                    else 
                    {
                        game.Map.ContainerSetFrame(targets[i].Container, "exp9");
                        game.Map.AnimationStart(targets[i].Container, "explosion", 1);
                        unit.removeObject();
                    }
                }
            }
        }
        void remove()
        {
            unit.removeObject();
        }
    }
}
