using GameMaps;
using System.Diagnostics;

namespace TowerDefence
{
    public class RotateTo : Behavior
    {
        protected ICoordinateProvider Target;
        protected UGameObjectBase gObj;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Объект, которым управляем</param>
        /// <param name="target">Цель</param>
        //public RotateTo(UGameObjectBase u, ICoordinateProvider target, string name) : base(u, name)
        //{
        //    Target = target;
        //}

        public RotateTo(ICoordinateProvider target)
        {
            Target = target;
        }

        public override void Act()
        {
            var delta = GameMath.GetRotationToTargetAngularChange(unit.Par.Angle,
                GameMath.GetAngleOfVector(Target.X - unit.Par.X, Target.Y - unit.Par.Y),
                unit.Par.AngularVelocity);
            unit.SetAngle((unit.Par.Angle + delta + 360) % 360);
        }
    }
}
