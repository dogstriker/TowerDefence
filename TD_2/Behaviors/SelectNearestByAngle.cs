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
                var d = GameMath.CompareAngles(unit.Par.Angle, GameMath.GetAngleOfVector(Target.Par.X-unit.Par.X, Target.Par.Y-unit.Par.Y));
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
                    deltaCounter = 0;
                }
                else
                {
                    lastDelta = d;
                }
            }
            else
            {
                SetTarget();
                deltaCounter = 0;
            }
        }

        void SetTarget()
        {
            double t, min = 361;
            Target = null;
            
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].Par.HP > 0)
                {
                    t = GameMath.CompareAngles(unit.Par.Angle, GameMath.GetAngleOfVector(targets[i].Par.X - unit.Par.X, targets[i].Par.Y - unit.Par.Y));
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
        }

        public override void Init(params object[] par)
        {
            SetTarget();
        }
    }
}
