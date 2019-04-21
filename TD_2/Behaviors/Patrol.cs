using GameMaps;
using System;
using System.Diagnostics;

namespace TowerDefence
{
    public class Patrol : BehaviorContainer
    {
        private ICoordinateProvider[] Points;
        private int currPoint;
        private int Prec;
        private bool Cycle;
        private double lastDistance = int.MaxValue;
        int countDist = 0;

        public Patrol(int prec, bool cycle, params ICoordinateProvider[] points )
        {
            Points = points;
            currPoint = 0;
            Prec = prec;
            Cycle = cycle;
        }

        public override void Act()
        {
            base.Act();
            double dx = Math.Abs(unit.Par.X - Points[currPoint].X), dy = Math.Abs(unit.Par.Y - Points[currPoint].Y);
            if (dx <= Prec && dy <= Prec)
            {
                NextPoint();
                LinkBehavior("rotateToPoint", new RotateTo(Points[currPoint]));
            }
            else
            {
                double d = dx * dx + dy * dy;
                if (d < lastDistance) lastDistance = d;
                else
                    countDist++;
                //Debug.WriteLine(countDist);
                if(countDist > 200)
                {
                    NextPoint();
                }

            }
        }

        private void NextPoint()
        {
            countDist = 0;
            lastDistance = int.MaxValue; if (currPoint < Points.Length - 1)
            {
                currPoint++;
            }
            else
            {
                if (Cycle)
                {
                    currPoint = 0;
                }
                else
                {
                    unit.RemoveBehavior(Name);
                }
            }
        }

        public override void Init(params object[] par)
        {
            LinkBehavior("moveForward", new MoveForward());
            LinkBehavior("rotateToPoint", new RotateTo(Points[0]));
        }
    }
}
