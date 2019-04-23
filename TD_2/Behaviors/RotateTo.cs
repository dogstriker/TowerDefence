using GameMaps;
using System.Diagnostics;

namespace TowerDefence
{
    public class RotateTo : Behavior
    {
        protected UGameObjectBase Target;
        protected ICoordinateProvider targetCoords;
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

        public RotateTo(UGameObjectBase target)
        {
            Target = target;
        }

        public RotateTo(ICoordinateProvider target)
        {
            targetCoords = target;
        }

        public override void Act()
        {
            if (Target != null)
            {
                var delta = GameMath.GetRotationToTargetAngularChange(unit.Par.Angle,
                    GameMath.GetAngleOfVector(Target.Par.X - unit.Par.X, Target.Par.Y - unit.Par.Y),
                    unit.Par.AngularVelocity);
                unit.SetAngle((unit.Par.Angle + delta + 360) % 360);
            }
            else if(targetCoords != null)
            {
                var delta = GameMath.GetRotationToTargetAngularChange(unit.Par.Angle,
                    GameMath.GetAngleOfVector(targetCoords.X - unit.Par.X, targetCoords.Y - unit.Par.Y),
                    unit.Par.AngularVelocity);
                unit.SetAngle((unit.Par.Angle + delta + 360) % 360);
            }

        }
    }
}
