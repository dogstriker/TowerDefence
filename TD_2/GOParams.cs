using System;
using GameMaps;
namespace TowerDefence
{
    public class GOParams : ICoordinateProvider
    {
        public bool IsFriendly { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public  int Range {get;set;}
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }
        public int ChargeRate { get; set; }
        public int ChargeLevel { get; set; }
        public int ChargeReady { get; set; }
        private double velocity;
        public double Velocity {
            get { return velocity; }
            set { velocity = value; UpdateXYVelocity(); }
        }
        public double AngularVelocity { get; set; }
        public double Vx { get; private set; }
        public double Vy { get; private set; }

        public double X { get; set; }
        public double Y { get; set; }

        private double angle;
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                UpdateXYVelocity();
            }
        }

        public void UpdateXYVelocity()
        {
            Vx = velocity * Math.Cos(GameMath.DegreesToRad(angle));
            Vy = velocity * Math.Sin(GameMath.DegreesToRad(angle));
        }

        public string debug;
    }


}
