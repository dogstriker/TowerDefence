using System.Collections.Generic;
using GameMaps;
namespace TowerDefence
{


    public class UGameObjectBase : IActor
    {
        private Dictionary<string, IBehavior> actions = new Dictionary<string, IBehavior>();
        private List<IBehavior> act = new List<IBehavior>();

        public GOParams Par { get; protected set; }
        public int Team { get; protected set; }

        static int count = 0;

        public string Container { get; protected set; }


        static public Game game;

        public UGameObjectBase(double x, double y, string PictureName, int team = 0)
        {
            Par = new GOParams();
            Team = team;
            Container = PictureName;
            Container = "u" + (++count).ToString();

            game.Map.Library.AddContainer(Container, PictureName, ContainerType.AutosizedSingleImage);
            //SetCoord(x, y);
            Par.X = x;
            Par.Y = y;
            game.Map.ContainerSetCoordinate(Container, x, y);

        }
        public void Click()
        {
            game.ClickedObj = this;
        }

        public void SetContainerSize(int Xsize, int Ysize)
        {
            game.Map.ContainerSetSize(Container, Xsize, Ysize);
        }

        public virtual void SetAngle(double angle)
        {
            Par.Angle = angle;
            game.Map.ContainerSetAngle(Container, (int)Par.Angle);
        }

        public virtual void SetCoord(double x, double y)
        {
            Par.X = x;
            Par.Y = y;
            game.Map.ContainerSetCoordinate(Container, x, y);
        }
        //public string GetContainerName()
        //{
        //    return Container;
        //}
        public void removeObject()
        {
            game.Map.ContainerRemove(Container);
            game.GameObjectsList.Remove(this);
            if (Par.IsFriendly) game.friendly.Remove(this);
            else game.enemies.Remove(this);
        }

        public void AddBehavior(IBehavior b, string behaviorName)
        {
            b.SetUnit(this, behaviorName);
            b.Init();
            if (actions.ContainsKey(b.Name)) actions[b.Name] = b;
            else actions.Add(b.Name, b);
            act.Add(b);
        }

        public void RemoveBehavior(string name)
        {
            if (actions.ContainsKey(name))
            {
                act.Remove(actions[name]);
                actions.Remove(name);
            }
        }

        public virtual void Act()
        {
            for (int i = 0; i < act.Count; i++)
            {
                act[i].Act();
            }
        }

        public void RemoveAllBehaviors()
        {
            actions.Clear();
        }
    }

}
