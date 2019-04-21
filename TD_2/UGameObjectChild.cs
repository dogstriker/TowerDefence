using GameMaps;
using System;

namespace TowerDefence
{
    public class UGameObjectChild : UGameObjectBase, IPositionObserver
    {
        private double lastAngle;
        public UGameObjectChild(double x, double y, double angle, double parentStartAngle, string PictureName, int team = 0) : base(x, y, PictureName, team)
        {
            SetAngle(angle);
            lastAngle = parentStartAngle;
        }

        public override void SetCoord(double x, double y)
        {
            Par.Par["dx"] = x;
            Par.Par["dy"] = y;
            var l = Math.Sqrt(x * x + y * y);
            Par.Par["__dL"] = l;
            Par.Par["__dA"] = GameMath.RadToDegrees(Math.Acos(x / l));
        }

        public void UpdatePosition(IPositionProvider pos)
        {
            //if (UpdateMode == PositionUpdateMode.Relative)
            {
                base.SetCoord(pos.X + Math.Cos(GameMath.DegreesToRad(pos.Angle + Par.Par["__dA"])) * Par.Par["__dL"],
                          pos.Y + Math.Sin(GameMath.DegreesToRad(pos.Angle + Par.Par["__dA"])) * Par.Par["__dL"]);
                if (pos.Angle != lastAngle)
                {
                    SetAngle(Par.Angle + pos.Angle - lastAngle);
                    lastAngle = pos.Angle;
                }

            }

        }

    }


}
