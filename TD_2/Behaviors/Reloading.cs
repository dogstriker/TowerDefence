namespace TowerDefence
{
    public class Reloading : Behavior
    {

        public override void Act()
        {
            unit.Par.ChargeLevel += unit.Par.ChargeRate;
        }
    }
}
