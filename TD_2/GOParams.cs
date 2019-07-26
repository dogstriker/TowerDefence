using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameMaps;
namespace TowerDefence
{
    public class GOParams : ICoordinateProvider, IPositionProvider
    {
        public int TTL { get; set; }
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
        public int Resources { get; set; }
        private double velocity;
        public Dictionary<string, double> Par=new Dictionary<string,double>();
        public double Velocity {
            get { return velocity; }
            set { velocity = value; UpdateXYVelocity(); }
        }
        public double AngularVelocity { get; set; }
        public double Vx { get; private set; }
        public double Vy { get; private set; }

        double _x;
        double _y;
        public double X {
            get { return _x; }
            set { _x = value; /*Debug.WriteLine("X changed to " + value.ToString());*/ } }
        public double Y {
            get { return _y; }
            set { _y = value; /*Debug.WriteLine("Y changed to " + value.ToString());*/ } }

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
            HP = p.HP;
            X = p.X;
            Y = p.Y;
            //foreach (var k in p.Par.Keys)
            //{
            //    if (Par.ContainsKey(k)) Par[k] = p.Par[k];
            //    else Par.Add(k, p.Par[k]);
            //}
        }

        public void CopyParWithoutPosition(GOParams p)
        {
            AngularVelocity = p.AngularVelocity;
            Attack = p.Attack;
            Defense = p.Defense;
            DamageMax = p.DamageMax;
            DamageMin = p.DamageMin;
            ChargeRate = p.ChargeRate;
            ChargeLevel = p.ChargeLevel;
            ChargeReady = p.ChargeReady;
            Velocity = p.Velocity;
            HP = p.HP;

            //foreach (var k in p.Par.Keys)
            //{
            //    if (Par.ContainsKey(k)) Par[k] = p.Par[k];
            //    else Par.Add(k, p.Par[k]);
            //}
        }

    }


    public class GOParamsRelative : GOParams
    {
        public new double X { get; set; }
    }

}
