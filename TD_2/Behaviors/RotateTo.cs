using GameMaps;
using System.Diagnostics;
using System;
namespace TowerDefence
{
    public class RotateTo : Behavior, IVelocityModifier
    {
        public double Modifier { get; private set; }
        protected ITargetProvider tarPr;
        protected ICoordinateProvider targetCoords;
        protected UGameObjectBase gObj;
        double ModifierValue;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Объект, которым управляем</param>
        /// <param name="target">Цель</param>
        //public RotateTo(UGameObjectBase u, ICoordinateProvider target, string name) : base(u, name)
        //{
        //    Target = target;
        //}

        public RotateTo(ITargetProvider target, double modifier)
        {
            ModifierValue = modifier;
            
            tarPr = target;
            Modifier = 1;
        }

        public RotateTo(ICoordinateProvider target, double modifier )
        {
            ModifierValue = modifier;

            targetCoords = target;
            Modifier = 1;

        }

        public override void Act()
        {
            double AngleOfVector;

            if (tarPr != null && tarPr.Target != null)
            {
                AngleOfVector = GameMath.GetAngleOfVector(tarPr.Target.Par.X - unit.Par.X, tarPr.Target.Par.Y - unit.Par.Y);
                var delta = GameMath.GetRotationToTargetAngularChange(unit.Par.Angle,
                   AngleOfVector,
                    unit.Par.AngularVelocity, 2);
                if (Math.Abs(AngleOfVector - unit.Par.Angle) > 75)
                {
                    Modifier = ModifierValue;
                }
                else
                {
                    Modifier = 1;
                }
                //Debug.WriteLine(string.Format("{0} A={1}, x={6}, y={7}, tx={2}, ty={3}, AT={4}, d={5}", unit.Container, unit.Par.Angle, Target.Par.X, Target.Par.Y,
                // GameMath.GetAngleOfVector(Target.Par.X - unit.Par.X, Target.Par.Y - unit.Par.Y), delta, unit.Par.X, unit.Par.Y));
                unit.SetAngle((unit.Par.Angle + delta + 360) % 360);
            
            }
            else if(targetCoords != null)
            {
                var delta = GameMath.GetRotationToTargetAngularChange(unit.Par.Angle,
                    GameMath.GetAngleOfVector(targetCoords.X - unit.Par.X, targetCoords.Y - unit.Par.Y),
                    unit.Par.AngularVelocity, 2);
                //Debug.WriteLine(string.Format("{0} A={1}, x={6}, y={7}, tx={2}, ty={3}, AT={4}, d={5}", unit.Container, unit.Par.Angle, targetCoords.X, targetCoords.Y,
                //GameMath.GetAngleOfVector(targetCoords.X - unit.Par.X, targetCoords.Y - unit.Par.Y), delta, unit.Par.X, unit.Par.Y));
                unit.SetAngle((unit.Par.Angle + delta + 360) % 360);
            }

        }
    }
}
