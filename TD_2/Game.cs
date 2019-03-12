using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaps;
namespace TowerDefence
{
    public class Game
    {
        public UniversalMap_Wpf Map;
        List<UGameObjectBase> GameObjectsList = new List<UGameObjectBase>();
        TimerController timer = new TimerController();
        List<List<UGameObjectBase>> teamUnits = new List<List<UGameObjectBase>>();
        List<UGameObjectBase> friendly = new List<UGameObjectBase>();
        List<UGameObjectBase> enemies = new List<UGameObjectBase>();
        //List<UGameObjectBase> bullets = new List<UGameObjectBase>();
        //List<UGameObjectBase> enemyBullets = new List<UGameObjectBase>();

        public UGameObjectBase Base;
        public Game(int teams)
        {
            for(int i = 0; i < teams; i++)
            {
                teamUnits.Add(new List<UGameObjectBase>());
            }
            timer.AddAction(EnemiesAct, 10);
            //timer.AddAction(checkEnemyHits, 10);
        }
        void EnemiesAct()
        {
            for(int i = 0; i < GameObjectsList.Count; i++)
            {
                GameObjectsList[i].Aсt();
            }
        }
        UGameObjectBase u;
        public void TestAdd()
        {
            u = new UGameObjectBase(20, 20, "platformRed1");
            u.Par.Velocity = 0.5;
            u.Par.AngularVelocity = 1;
            GameObjectsList.Add(u);
            u.SetCoord(400, 200);
            u.SetAngle(-30);
            timer.AddAction(change, 2000);
            //u.SetCoord(100, 300);
            //u.AddBehavior("1", new MoveTo(u, 300, 300, 2));
        }

        static int acct =0;
        public void change()
        {
            switch(acct)
            {
                case 0:
                    acct = 1;
                    break;
                case 1:
                    u.RemoveBehavior("3");
                    u.AddBehavior("1", new MoveTo(u, new DoubleCoordinate(100,100), 2));
                    acct = 2;
                    break;
                case 2:
                    u.RemoveBehavior("1");
                    u.AddBehavior("2", new RotateTo(u, new DoubleCoordinate(0, 0)));
                    acct = 3;
                    break;
                case 3:
                    u.RemoveBehavior("2");
                    u.AddBehavior("3", new MoveForward(u));
                    acct = 0;
                    break;

            }
        }

        public void AddBase(double X, double Y, string picture)
        { 
            Base = new UGameObjectBase(X,Y,picture);
            GameObjectsList.Add(Base);
            Base.SetContainerSize(100, 100);
            friendly.Add(Base);
        }

        public void AddObject(string name, GOParams par)
        {
            UGameObjectBase u = null;
            switch(name)
            {
                case "SimpleFlyer":
                    u = new UGameObjectBase(par.X, par.Y, "flyerRed1", 1);
                    u.Par.Velocity = 3;
                    u.Par.AngularVelocity = 1;
                    u.SetAngle(-90);
                    u.AddBehavior("ctrl",new ControlSimpleFlyer(u));
                    u.Par.ChargeLevel = 330;
                    u.Par.ChargeReady = 330;
                    u.Par.ChargeRate = 10;
                    u.AddBehavior("shoot", new ShootWhenAimed(u, Base.Par.X, Base.Par.Y));
                    break;
                case "Rocket":
                    u = new UGameObjectBase(par.X, par.Y, "MissileRed1", 1);
                    u.Par.Velocity = 8;
                    u.SetAngle(par.Angle);
                    u.AddBehavior("Move", new MoveForward(u));
                    break;
            }

            if (u != null)
            {
                GameObjectsList.Add(u);
                teamUnits[u.Team].Add(u);
            }
        }
    
        //public void checkEnemyHits()
        //{
        //    for (int i = 0; i < enemyBullets.Count; i++)
        //    {
        //        for (int j = 0; j < friendly.Count; j++)
        //        { 
        //            if(Map.CollisionContainers(enemyBullets[i].GetContainerName(),friendly[j].GetContainerName()))
        //            {
        //                enemyBullets[i].movement = null;
        //                enemyBullets[i].SetContainerSize(0, 50,50);

        //                enemyBullets[i].SetAngle(0);
        //                Map.AnimationStart(enemyBullets[i].GetContainerName(), "explosion", 1, enemyBullets[i].removeObject);
        //                var d = Map.Library.GetContainerDescription(enemyBullets[i].GetContainerName());
        //                enemyBullets.RemoveAt(i);
        //                i--;
        //                break;
                        
        //            }
        //        }
        //    }
        //}
        
    }
}
