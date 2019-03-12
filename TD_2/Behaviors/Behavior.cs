namespace TowerDefence
{
    public abstract class Behavior : IBehavior
    {
        static public Game game { get; set; }
        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name.Length == 0) _name = value;
            }
        }
        protected UGameObjectBase unit;
        public Behavior(UGameObjectBase _g)
        {
            unit = _g;
        }

        public abstract void Act();
    }
}
