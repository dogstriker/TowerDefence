using System.Collections.Generic;
using GameMaps;
namespace TowerDefence
{


    public class UGameObjectBase : IActor, ITargetProvider
    {
        private Dictionary<string, IBehavior> actions = new Dictionary<string, IBehavior>();
        private List<IBehavior> act = new List<IBehavior>();

        public GOParams Par { get; protected set; }
        public int Team { get; protected set; }

        static int count = 0;

        public string Container { get; protected set; }

        public UGameObjectBase Target { get { return this; } }

        public string destroyedPictureName;

        static public Game game;

        public clickerBase clicked;

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
        public void RightClick()
        {
            if (game.ClickedObj != null)
            {
                
                UCompositeGameObject o = (UCompositeGameObject)game.ClickedObj;
                for (int i = 0; i < o.Children.Count; i++)
                {
                    o.Children[i].RemoveBehavior("SelectNearestByAngle");
                    o.Children[i].RemoveBehavior("RotateTo");
                    o.Children[i].RemoveBehavior("ShootWhenAimed");
                    o.Children[i].AddBehavior(new RotateTo(this.Par), "RotateTo");
                    o.Children[i].AddBehavior(new ShootWhenAimed(this, o.Children[i].Par.ParString["ShellName"], game.enemies), "ShootWhenAimed");
                }
            }
            
        }
        
        public void Click()
        {
           // game.ClickedObj = this;
            clicked.Clicked(this);
            game.ClickTimeCount = 5;
        }

        public void SetContainerSize(int Xsize, int Ysize)
        {
            game.Map.ContainerSetSize(Container, Xsize, Ysize);
        }
        public void Destroyed()
        {
            game.Map.ContainerSetFrame(Container, destroyedPictureName);
            game.Map.ContainerSetMaxSide(Container, 60);
            UCompositeGameObject c = this as UCompositeGameObject;
            if (c != null)
            {
                for (int j = 0; j < c.Children.Count; j++)
                {
                    c.Children[j].removeObject();

                }


            }
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
        public IBehavior GetBehavior(string BehaviorName)
        {
            if (actions.ContainsKey(BehaviorName))
            {
                return actions[BehaviorName];
            }
            else
            {
                return null;
            }
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
            act.Clear();
        }
    }

}
