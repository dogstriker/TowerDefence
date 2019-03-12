using System.Diagnostics;
namespace TowerDefence
{
    public class MoveForward : Behavior
    {
        public MoveForward(UGameObjectBase g) : base(g)
        {
        }

        public override void Act()
        {
           // Debug.WriteLine("MoveForward");
            unit.SetCoord(unit.Par.X + unit.Par.Vx, unit.Par.Y + unit.Par.Vy);
        }
    }
}
