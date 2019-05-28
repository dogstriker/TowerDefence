using GameMaps;
using System;
using System.Collections.Generic;

namespace TowerDefence
{
    public class UGameObjectChild : UGameObjectBase, IPositionObserver
    {
        /// <summary>
        /// При обновлении позиции проверяем, изменился ли угол родителя
        /// </summary>
        private double lastParentAngle;
        private IPositionProvider ParentPosProvider;
        private double ownAngle;
        public UGameObjectChild(IPositionProvider parentPos, double childX, double childY, string PictureName, int team = 0) : base(-10000, -10000, PictureName, team)
        {
            ParentPosProvider = parentPos;
            lastParentAngle = parentPos.Angle;
            Par.Par.Add("dx", 0);
            Par.Par.Add("dy", 0);
            Par.Par.Add("__dL", 0);
            Par.Par.Add("__dA", 0);
        }

        //public override void SetCoord(double x, double y)
        //{
        //    Par.Par["dx"] = x;
        //    Par.Par["dy"] = y;
        //    var l = Math.Sqrt(x * x + y * y);
        //    Par.Par["__dL"] = l;
        //    Par.Par["__dA"] = GameMath.RadToDegrees(Math.Acos(Math.Abs(x) / l));
        //    if(x < 0 && y > 0)
        //    {
        //        Par.Par["__dA"] = 180 - Par.Par["__dA"];
        //    }
        //    else if(x < 0 && y < 0)
        //    {
        //        Par.Par["__dA"] += 180;
        //    }
        //    else if(x > 0 && y < 0)
        //    {
        //        Par.Par["__dA"] = 360 - Par.Par["__dA"];
        //    }

        //}
        private double _x;
        private double _y;

        public override void SetCoord(double x, double y)
        {
            Par.Par["dx"] = x;
            Par.Par["dy"] = y;
            var l = Math.Sqrt(x * x + y * y);
            Par.Par["__dL"] = l;
            Par.Par["__dA"] = GameMath.RadToDegrees(Math.Acos(Math.Abs(x) / l));
            if (x < 0 && y > 0)
            {
                Par.Par["__dA"] = 180 - Par.Par["__dA"];
            }
            else if (x < 0 && y < 0)
            {
                Par.Par["__dA"] += 180;
            }
            else if (x > 0 && y < 0)
            {
                Par.Par["__dA"] = 360 - Par.Par["__dA"];
            }
            CalculateCoords();

        }

        //public override void SetAngle(double angle)
        //{
        //    ownAngle = angle;
        //    Par.Angle = angle + ParentPosProvider.Angle;
        //    base.SetAngle(Par.Angle);

        //}

        void CalculateCoords()
        {
            _x = ParentPosProvider.X + Math.Cos(GameMath.DegreesToRad(ParentPosProvider.Angle + Par.Par["__dA"])) * Par.Par["__dL"];
            _y = ParentPosProvider.Y + Math.Sin(GameMath.DegreesToRad(ParentPosProvider.Angle + Par.Par["__dA"])) * Par.Par["__dL"];
            Par.X = _x;
            Par.Y = _y;
            //Par.Angle = ParentPosProvider.Angle + ownAngle;

        }

        public void UpdatePosition()
        {
            //if (UpdateMode == PositionUpdateMode.Relative)
            {
                CalculateCoords();
                //base.SetCoord(ParentPosProvider.X + Math.Cos(GameMath.DegreesToRad(ParentPosProvider.Angle + Par.Par["__dA"])) * Par.Par["__dL"],
                //         ParentPosProvider.Y + Math.Sin(GameMath.DegreesToRad(ParentPosProvider.Angle + Par.Par["__dA"])) * Par.Par["__dL"]);
                base.SetCoord(Par.X, Par.Y);
                //SetAngle(ownAngle); // внутри будет учтено изменение угла
                if (ParentPosProvider.Angle != lastParentAngle)
                {
                    SetAngle(Par.Angle + ParentPosProvider.Angle - lastParentAngle);
                    lastParentAngle = ParentPosProvider.Angle;
                }

            }

        }

    }


}
