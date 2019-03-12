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
        public RotateTo(UGameObjectBase obj, ICoordinateProvider target) : base(obj)
        {
            Target = target;
            gObj = obj;
        }

        public override void Act()
        {
           // Debug.WriteLine("RotateTo");
            var delta = GameMath.GetRotationToTargetAngularChange(gObj.Par.Angle,
                GameMath.GetAngleOfVector(Target.X - gObj.Par.X, Target.Y - gObj.Par.Y),
                gObj.Par.AngularVelocity);
            gObj.SetAngle((gObj.Par.Angle + delta + 360) % 360);
        }
    }
}
