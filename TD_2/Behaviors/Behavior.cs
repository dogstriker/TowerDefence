namespace TowerDefence
{
    public abstract class Behavior : IBehavior
    {
        static public Game game { get; set; }

        //public Behavior(string bName)
        //{
        //    Name = bName;
        //}

        public string Name { get; protected set; }

        protected UGameObjectBase unit = null;

        public void SetUnit(UGameObjectBase g, string behaviorName)
        {
            if (unit == null)
            {
                unit = g;
                Name = behaviorName;
            }
            else
            {
                throw new System.Exception("Невозможно повторно назаначить объект управления.");
            }
        }

        public abstract void Act();

        public virtual void Init(params object[] par)
        { 
        }

        public virtual void RemoveNested()
        {
        }
    }
}
