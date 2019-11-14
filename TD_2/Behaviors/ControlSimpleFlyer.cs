using GameMaps;

namespace TowerDefence
{
    public class ControlSimpleFlyer : Behavior
    {
        ICoordinateProvider Target;
        public ControlSimpleFlyer() //: base(u, name)
        {

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
                    if (GameMath.IsNearby(unit.Par, Target))
                    {
                        unit.RemoveBehavior("rotate");
                        State = 2;
                    }
                    break;
                case 2:
                    if (!GameMath.IsNearby(unit.Par, Target, 150))
                    {
                        GetTarget();
                        unit.AddBehavior(new RotateTo(Target,1), "rotate");
                        State = 1;
                    }
                    break;
            }
        }

        void GetTarget()
        {
            // пока что цель - это всегда база.
            Target = game.Base.Par;
        }

        public override void Init(params object[] par)
        {
            // задаем цель
            GetTarget();
            // поведение "двигаться вперед" у флаера есть всегда
            unit.AddBehavior(new MoveForward(), "moveForvard");

            // задаем начальное состояние и поведение
            State = 1;
            unit.AddBehavior(new RotateTo(Target,1), "rotate");
        }
    }
}
