using System;
using System.Collections.Generic;
using GameMaps;
namespace TowerDefence
{
    public class GOParams : ICoordinateProvider, IPositionProvider
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
        public Dictionary<string, double> Par=new Dictionary<string,double>();
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

        public GOParams()
        {
            Par = new Dictionary<string, double>
            {
                { "dx", 0 },
                { "dy", 0 },
                { "__dL", 0},
                {"__dA", 0 }
            };
        }

        public void CopyPar(GOParams p)
        {
            Angle = p.Angle;
            AngularVelocity = p.AngularVelocity;
            Attack = p.Attack;
            Defense = p.Defense;
            DamageMax = p.DamageMax;
            DamageMin = p.DamageMin;
            ChargeRate = p.ChargeRate;
            ChargeLevel = p.ChargeLevel;
            ChargeReady = p.ChargeReady;
            Velocity = p.Velocity;
            X = p.X;
            Y = p.Y;
            //foreach (var k in p.Par.Keys)
            //{
            //    if (Par.ContainsKey(k)) Par[k] = p.Par[k];
            //    else Par.Add(k, p.Par[k]);
            //}
        }
    }


}
