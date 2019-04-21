using System.Collections.Generic;

namespace TowerDefence
{
    public class UCompositeGameObject : UGameObjectBase, IPositionObservable
    {
        public UCompositeGameObject(double x, double y, string PictureName, int team = 0) : base(x, y, PictureName, team)
        {
            Children = new List<UGameObjectChild>();
        }

        public List<UGameObjectChild> Children { get; private set; }

        public override void Act()
        {
            base.Act();
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Act();
            }
        }

        public void AddChild(double x, double y, double angle, string PictureName)
        {
            var c = new UGameObjectChild(Par.X + x, Par.Y + y, angle, Par.Angle, PictureName);
            //c.Par.Par["dx"] = x;
            //c.Par.Par["dy"] = y;
            c.SetCoord(x, y);
            RegisterObserver(c);
            c.UpdatePosition(Par);
            Children.Add(c);

        }

        public override void SetCoord(double x, double y)
        {
            base.SetCoord(x, y);
            NotifyObservers();
        }

        public override void SetAngle(double angle)
        {
            base.SetAngle(angle);
            NotifyObservers();
        }

        #region Observable

        List<IPositionObserver> positionObservers = new List<IPositionObserver>();

        public void RegisterObserver(IPositionObserver ob)
        {
            positionObservers.Add(ob);
        }

        public void RemoveObserver(IPositionObserver ob)
        {
            try
            {
                positionObservers.Remove(ob);
            }
            catch { }
        }

        public void NotifyObservers()
        {
            for (int i = 0; i < positionObservers.Count; i++)
            {
                positionObservers[i].UpdatePosition(Par);
            }
        }



        #endregion
    }


}
