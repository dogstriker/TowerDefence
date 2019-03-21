namespace TowerDefence
{
    public class PeriodicRotate : Behavior
    {
        double Limit, Delta, currDelta;
        int deltaSign = 1;
        //public PeriodicRotate(UGameObjectBase _g, double limit, double delta, string bName) : base(_g, bName)
        //{
        //    Limit = limit;
        //    Delta = delta;
        //}

        public PeriodicRotate(double limit, double delta)
        {
            Limit = limit;
            Delta = delta;
        }
        public override void Act()
        {
            currDelta += deltaSign*Delta;
            unit.SetAngle(unit.Par.Angle + deltaSign*Delta);
            if(currDelta > Limit || currDelta < -Limit)
            {
                deltaSign *= -1;
            }
        }
    }
}
