using System;
using System.Collections.Generic;

namespace TowerDefence
{
    public class BehaviorContainer : Behavior
    {
        public Dictionary<string, IBehavior> Behaviors { get; }

        public BehaviorContainer(/*UGameObjectBase _g, string bName*/)// : base(_g, bName)
        {
            Behaviors = new Dictionary<string, IBehavior>();
            // не будет работать удаление вложенных поведений, т.к. они привязываются при new, но отязываются вручную
            //throw new Exception();
        }

        public override void Act()
        {
            foreach(var key in Behaviors.Keys)
            {
                Behaviors[key].Act();
            }
        }

        protected void LinkBehavior(string name, IBehavior behavior)
        {
            if(Behaviors.ContainsKey(name))
            {
                Behaviors[name] = behavior;
            }
            else
            {
                Behaviors.Add(name, behavior);
            }
            behavior.SetUnit(unit, name);
        }
    }
}
