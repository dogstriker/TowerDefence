namespace TowerDefence
{
    public class HitHumanUnit : Behavior
    {
        //public HitHumanUnit(UGameObjectBase _g, string name) : base(_g, name)
        //{
        //}

        public override void Act()
        {
            int i = 0;
            while(i < game.friendly.Count)
            {
                if(game.Map.CollisionContainers(unit.Container, game.friendly[i].Container))
                {
                    unit.RemoveBehavior("move");
                    unit.RemoveBehavior("hit");
                    unit.SetContainerSize(40, 40);
                    game.Map.AnimationStart(unit.Container, "explosion", 1, RemoveMissile);
                    break;
                }
                else
                {
                    i++;
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
