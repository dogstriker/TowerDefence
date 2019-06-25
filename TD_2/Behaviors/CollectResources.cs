using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    class CollectResources:Behavior
    {
        SelectNearestByRange r;
        public CollectResources(SelectNearestByRange s)
        {
            r = s;
        }
        public override void Act()
        {
            if (r.Target!=null&&r.Target.Par.HP <= 0 && game.Map.CollisionContainers(r.Target.Container, (unit as UCompositeGameObject).Children[0].Container) && r.Target.Par.Resources>0)
            {
                unit.Par.Resources++;
                r.Target.Par.Resources--;
            }
            if (game.Map.CollisionContainers(unit.Container, game.Base.Container) && unit.Par.Resources > 100)
            {
                game.totalResources += unit.Par.Resources - 100;
                unit.Par.Resources = 100;
            }
        }
    }
}
