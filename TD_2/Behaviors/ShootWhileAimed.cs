using GameMaps;
using System;
namespace TowerDefence
{
    public class ShootWhileAimed : Behavior
    {
        ICoordinateProvider target;
        //public ShootWhileAimed(UGameObjectBase u, ICoordinateProvider _target, string behaviorName) : base(u, behaviorName)
        //{
        //    target = _target;
        //}

        public ShootWhileAimed(ICoordinateProvider _target)
        {
            target = _target;
        }

        public override void Act()
        {
            if(unit.Par.ChargeLevel >= unit.Par.ChargeReady)
            {
                var d = Math.Abs(unit.Par.Angle - GameMath.GetAngleOfVector(target.X - unit.Par.X, target.Y - unit.Par.Y));
                if (Math.Abs(unit.Par.Angle - GameMath.GetAngleOfVector(target.X - unit.Par.X, target.Y - unit.Par.Y)) <= 2)
                {
                    game.AddObject("Rocket", unit.Par);
                    unit.Par.ChargeLevel = 0;
                }
            }

            //проверяем, заряжена ли ракета
            //проверяем, наведен ли флаер на цель
            //да: стреляем.
                // добавляем объект "ракета" (уже есть в AddObject)
                // сбрасываем заряд в 0
        }
    }
}
