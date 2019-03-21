using System;
using GameMaps;
using System.Diagnostics;

namespace TowerDefence
{
    public class MoveTo : Behavior
    {
        double dx, dy, precision, tx, ty;
        ICoordinateProvider target;
        //public MoveTo(UGameObjectBase u,ICoordinateProvider _target, string name, int prec = 2) : base(u, name)
        //{
        //    target = _target;
        //    SetDelta();
        //    precision = prec;
        //}

        public MoveTo(ICoordinateProvider _target, int prec = 2)
        {
            target = _target;
            precision = prec;
        }

        void SetDelta()
        {
            tx = target.X;
            ty = target.Y;
            var a = GameMath.GetAngleOfVector(target.X - unit.Par.X, target.Y - unit.Par.Y);
            dx = Math.Cos(GameMath.DegreesToRad(a)) * unit.Par.Velocity;
            dy = Math.Sin(GameMath.DegreesToRad(a)) * unit.Par.Velocity;
        }

        public override void Act()
        {
            if(Math.Abs(target.X - tx) > precision || Math.Abs(target.Y - ty) > precision)
            {
                SetDelta();
            }

            if (Math.Abs(unit.Par.X - tx) > precision || Math.Abs(unit.Par.Y - ty) > precision)
            {
                unit.SetCoord(unit.Par.X + dx, unit.Par.Y + dy);
                //Debug.WriteLine("{0} | {1} | {2}", g.X, dx, g.X + dx);
            }
            else
            {
                unit.RemoveBehavior(Name);
            }

        }

        public override void Init(params object[] par)
        {
            SetDelta();
        }
    }
}
