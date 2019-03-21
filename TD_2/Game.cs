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
        public List<UGameObjectBase> GameObjectsList = new List<UGameObjectBase>();
        TimerController timer = new TimerController();
        List<List<UGameObjectBase>> teamUnits = new List<List<UGameObjectBase>>();
        public List<UGameObjectBase> friendly = new List<UGameObjectBase>();
        public List<UGameObjectBase> enemies = new List<UGameObjectBase>();
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
                GameObjectsList[i].Act();
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
                    u.SetContainerSize(40, 40);
                    u.Par.Velocity = 3;
                    u.Par.AngularVelocity = 1;
                    u.SetAngle(-90);
                    u.AddBehavior(new ControlSimpleFlyer(), "ctrl");
                    u.Par.ChargeLevel = 330;
                    u.Par.ChargeReady = 330;
                    u.Par.ChargeRate = 10;
                    u.AddBehavior(new ShootWhenAimed(Base.Par.X, Base.Par.Y), "Shoot");
                    u.AddBehavior(new Reloading(), "Reloadng");
                    
                    break;
                case "Rocket":
                    u = new UGameObjectBase(par.X, par.Y, "MissileRed1", 1);
                    u.SetContainerSize(30, 12);
                    u.Par.Velocity = 8;
                    u.SetAngle(par.Angle);
                    u.AddBehavior(new MoveForward(), "Move");
                    u.AddBehavior(new Hit(Base), "Hit");
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
