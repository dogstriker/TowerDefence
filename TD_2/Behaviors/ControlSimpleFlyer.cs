using GameMaps;

namespace TowerDefence
{
    public class ControlSimpleFlyer : Behavior
    {
        ICoordinateProvider Target;
        public ControlSimpleFlyer(UGameObjectBase u) : base(u)
        {

            // задаем цель
            GetTarget();
            // поведение "двигаться вперед" у флаера есть всегда
            unit.AddBehavior("MoveForward",new MoveForward(unit));
            // задаем начальное состояние и поведение
            unit.AddBehavior("Rotate", new RotateTo(unit, Target));
            State = 1;
        }

        int State;

        /// <summary>
        /// Состояния флаера:
        /// 1 - наводимся на цель. Смена на 2, когда пролетели над целью.
        /// 2 - Летим вперед, пока не удалимся от цели на 150 точек по одной из координат, затем 1
        /// </summary>
        public override void Act()
        {
            switch (State)
            {
                case 1:
                    if(GameMath.IsNearby(unit.Par,Target))
                    {
                        State = 2;
                        unit.RemoveBehavior("Rotate");
                    }
                    break;
                case 2:
                    if (!GameMath.IsNearby(unit.Par, Target, 150))
                    {
                        State = 1;
                        unit.AddBehavior("Rotate", new RotateTo(unit, Target));
                    }
                    break;
            }
        }

        void GetTarget()
        {
            // пока что цель - это всегда база.
            Target = game.Base.Par;
        }
    }
}
