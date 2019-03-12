using System.Collections.Generic;
using GameMaps;
namespace TowerDefence
{
    public class UGameObjectBase
    {
        private Dictionary<string, IBehavior> actions = new Dictionary<string, IBehavior>();
        private List<IBehavior> act = new List<IBehavior>();


        public GOParams Par { get; protected set; }
        public int Team { get; protected set; }



        static int count = 0;

        protected string container;

        static public Game game;

        public UGameObjectBase(double x, double y, string PictureName, int team = 0)
        {
            Par = new GOParams();
            Team = team;
            container = PictureName;
            container = "u" + (++count).ToString();

            game.Map.Library.AddContainer(container, PictureName, true, game.Map.CellSize);
            SetCoord(x, y);

        }
        public void SetContainerSize(int Xsize, int Ysize)
        {
            game.Map.ContainerSetSize(container, Xsize, Ysize);
        }

        public void SetAngle(double angle)
        {
            Par.Angle = angle;
            game.Map.ContainerSetAngle(container, (int)Par.Angle);
        }
 
        public void SetCoord(double x, double y)
        {
            Par.X = x;
            Par.Y = y;
            game.Map.ContainerSetCoordinate(container, x, y);
        }
        public string GetContainerName()
        {
            return container;
        }
        public void removeObject()
        {
            game.Map.ContainerRemove(container);
        }

        public void AddBehavior(string name, IBehavior b)
        {
            if (actions.ContainsKey(name)) actions[name] = b;
            else actions.Add(name, b);
            b.Name = name;
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

        public void Aсt()
        {
            for(int i = 0; i < act.Count; i++)
            {
                act[i].Act();
            }
        }
    }


}
