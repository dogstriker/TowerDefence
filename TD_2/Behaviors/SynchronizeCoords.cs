using GameMaps;

namespace TowerDefence
{
    public class SynchronizeCoords : Behavior
    {
        ICoordinateProvider master;
        //public SynchronizeCoords(UGameObjectBase u,ICoordinateProvider _master, string name) : base(u, name)
        //{
        //    master = _master;
        //}

        public SynchronizeCoords(ICoordinateProvider _master)
        {
            master = _master;
        }

        public override void Act()
        {
            if(master.X != unit.Par.X || master.Y != unit.Par.Y)
                unit.SetCoord(master.X, master.Y);
        }
    }
}
