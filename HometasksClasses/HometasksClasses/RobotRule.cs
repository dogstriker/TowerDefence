namespace HometasksClasses
{
    public class RobotRule
    {
        public char MapElement { get; }
        public RobotStateChange State { get; }

        public RobotRule(char _mapElement, RobotStateChange _state)
        {
            MapElement = _mapElement;
            State = _state;
        }
    }

}
