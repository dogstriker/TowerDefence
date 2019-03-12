namespace TowerDefence
{
    public class Reloading : Behavior
    {
        public Reloading(UGameObjectBase obj):base(obj)
        {
            
        }

        public override void Act()
        {
            unit.Par.ChargeLevel += unit.Par.ChargeRate;
        }
    }
}
