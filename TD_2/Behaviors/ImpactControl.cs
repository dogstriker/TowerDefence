using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    class ImpactControl:Behavior
    {
        public override void Act()
        {
            for (int i = 0; i < game.GameObjectsList.Count; i++)
            {
                if (unit.Container != game.GameObjectsList[i].Container&&
                    game.Map.CollisionContainers(unit.Container, game.GameObjectsList[i].Container) &&
                    game.GameObjectsList[i].Par.Type == UnitTypes.ground  )
                {
                    if (unit.Par.Mass - game.GameObjectsList[i].Par.Mass > 800)
                    {
                        game.GameObjectsList[i].Par.HP = 0;
                        game.GameObjectsList[i].StartDestroy();
                    }
                }
            } 
        }
    }
}
