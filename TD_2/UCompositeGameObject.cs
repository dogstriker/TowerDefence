using System.Collections.Generic;

namespace TowerDefence
{
    public class UCompositeGameObject : UGameObjectBase
    {
        public UCompositeGameObject(double x, double y, string PictureName, int team = 0) : base (x,y,PictureName,team)
        {
            Children = new List<UGameObjectBase>();
        }

        public List<UGameObjectBase> Children { get; }

        public override void Act()
        {
            base.Act();
            for(int i = 0; i < Children.Count; i++)
            {
                Children[i].Act();
            }
        }
    }


}
