using System.Collections.Generic;

namespace TowerDefence
{
    public class SimpleHit : Behavior
    {
        string Animation = "";
        List<UGameObjectBase> Targets;

        public SimpleHit(/*UGameObjectBase u, string name,*/bool IsPlayer, string animtion) /*: base(u, name)*/
        {
            Animation = animtion;
            if (IsPlayer) Targets = game.enemies;
            else Targets = game.friendly;
        }

        public override void Act()
        {
            for(int i = 0; i < Targets.Count; i++)
            {
                if (game.Map.CollisionContainers(unit.Container, Targets[i].Container))
                {
                    unit.RemoveBehavior("move");
                    unit.RemoveBehavior("hit");
                    unit.SetContainerSize(40, 40);
                    if (Animation.Length > 0)
                    {
                        game.Map.AnimationStart(unit.Container, Animation, 1, RemoveMissile);
                    }
                    else
                    {
                        RemoveMissile();
                    }
                    break;
                }
            }
        }

        void RemoveMissile()
        {
            unit.removeObject();
            game.GameObjectsList.Remove(unit);
        }
    }
}
