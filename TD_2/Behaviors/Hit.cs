using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaps;
namespace TowerDefence 
{
    public class Hit : Behavior
    {
        UGameObjectBase target;
        public Hit(UGameObjectBase Target)
        {
            target = Target;
        }
        public override void Act()
        {
            if(game.Map.CollisionContainers(unit.Container, target.Container))
            {
                unit.RemoveBehavior("Hit");
                unit.RemoveBehavior("Move");
                unit.SetContainerSize(40, 40);
                game.Map.AnimationStart(unit.Container, "explosion", 1,remove);

            }
        }
        void remove()
        {
            unit.removeObject();
        }
    }
}
