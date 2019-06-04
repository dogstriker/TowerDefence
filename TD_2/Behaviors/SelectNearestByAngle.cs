using System.Collections.Generic;
using GameMaps;

namespace TowerDefence
{
    public class SelectNearestByAngle : Behavior, ITargetProvider
    {
        public UGameObjectBase Target { get; private set; }
        List<UGameObjectBase> targets;
        int deltaCounter;
        double lastDelta;
        //public SelectNearestByAngle(UGameObjectBase _g, bool isPlayer, string bName) : base(_g, bName)
        //{
        //    if (isPlayer) targets = game.enemies;
        //    else targets = game.friendly;
        //    SetTarget();
        //}

        public SelectNearestByAngle(List<UGameObjectBase> targetsList)
        {
            targets = targetsList;
        }

        public override void Act()
        {

            if (Target != null && Target.Par.HP > 0)
            {
                var d = GameMath.CompareAngles(unit.Par.Angle, Target.Par.Angle);
                if (d > lastDelta)
                {
                    deltaCounter++;
                }
                else
                {
                    deltaCounter = 0;
                }
                if (deltaCounter > 30)
                {
                    lastDelta = 1000;
                    SetTarget();
                }
                else
                {
                    lastDelta = d;
                }
            }
            else
            {
                SetTarget();
            
            }
        }

        void SetTarget()
        {
            double t, min = 361;
            Target = null;
            for (int i = 0; i < targets.Count; i++)
            {
                t = GameMath.CompareAngles(unit.Par.Angle, targets[i].Par.Angle);
                if (t < min)
                {
                    min = t;
                    //if(currTarget != null)
                    //game.Map.ContainerSetFrame(currTarget.GetContainerName(), "flyerRed3");
                    Target = targets[i];
                    //game.Map.ContainerSetFrame(currTarget.GetContainerName(), "flyerRed2");
                }
            }
        }

        public override void Init(params object[] par)
        {
            SetTarget();
        }
    }
}
