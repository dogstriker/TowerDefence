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
        public UGameObjectBase ClickedObj;


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
        UCompositeGameObject AddTank(string[] picList, GOParams[] par)
        {
            UCompositeGameObject tank = new UCompositeGameObject(par[0].X, par[0].Y, picList[0]);
            Map.ContainerSetMaxSide(tank.Container,(int)par[0].Par["maxSide"]);
            tank.AddChild(0, 0, 0, picList[1]);

            Map.ContainerSetMaxSide(tank.Children[0].Container, (int)par[1].Par["maxSide"]);
            return tank;
        }
        public void CreateTank(string tankName, int x, int y)
        {
            UCompositeGameObject tank;
            GOParams[] p;
            SelectNearestByAngle v;
            switch (tankName)
            { 
                    
                    
                        
                case "allyLightTank":
                        p=new GOParams []{
                        new GOParams {X=x,Y=y,Velocity=1,AngularVelocity=1},
                        new GOParams{X=x,Y=y,AngularVelocity=1.5,ChargeLevel=1000,ChargeReady=1000,ChargeRate=40}};
                    p[0].Par.Add("maxSide", 60);
                    p[1].Par.Add("maxSide", 60);
                    tank= AddTank(new string[] { "platformSand1", "towerSand3" },p);
                    v = new SelectNearestByAngle(enemies);
                    tank.Par.CopyPar (p[0]);
                    tank.Children[0].Par.CopyPar ( p[1]);
                       //добавить танк в списки союзников и игровых обьектов
                       // слежение за целью и выстрел для башни
                    tank.Children[0].AddBehavior(v, "SelectNearestByAngle");
                    tank.Children[0].AddBehavior(new RotateTo(v.currTarget), "RotateTo");
                    tank.Children[0].AddBehavior(new ShootWhenAimed(v.currTarget,"LightShell",enemies), "ShootWhenAimed");
                    tank.Children[0].AddBehavior(new Reloading(), "Reloading");
                    //tank.Children[0].AddBehavior(new SynchronizeCoords(tank.Par), "SynchronizeCoords");
                    Map.ContainerSetLeftClickHandler(tank.Container, tank.Click);
                    friendly.Add(tank);
                    GameObjectsList.Add(tank);
                    break;
                case "allyMediumTank":
                        p=new GOParams []{
                        new GOParams {X=x,Y=y,Velocity=0.8,AngularVelocity=0.8},
                        new GOParams{X=x,Y=y,AngularVelocity=1.1,ChargeLevel=1200,ChargeReady=1200,ChargeRate=30}};
                    p[0].Par.Add("maxSide", 80);
                    p[1].Par.Add("maxSide", 80);
                    tank= AddTank(new string[] { "platformSand3", "towerSand4" },p);
                    v = new SelectNearestByAngle(enemies);
                    tank.Par.CopyPar ( p[0]);
                    tank.Children[0].Par.CopyPar ( p[1]);
                       //добавить танк в списки союзников и игровых обьектов
                       // слежение за целью и выстрел для башни
                    tank.Children[0].AddBehavior(v, "SelectNearestByAngle");
                    tank.Children[0].AddBehavior(new RotateTo(v.currTarget), "RotateTo");
                    tank.Children[0].AddBehavior(new ShootWhenAimed(v.currTarget,"LightShell",enemies), "ShootWhenAimed");
                    tank.Children[0].AddBehavior(new Reloading(), "Reloading");
                    //tank.Children[0].AddBehavior(new SynchronizeCoords(tank.Par), "SynchronizeCoords");
                    Map.ContainerSetLeftClickHandler(tank.Container, tank.Click);
                    friendly.Add(tank);
                    GameObjectsList.Add(tank);
                    break;
                case "enemyLightTank":
                     p=new GOParams []{
                        new GOParams {X=x,Y=y,Velocity=1,AngularVelocity=1},
                        new GOParams{X=x,Y=y,AngularVelocity=3.5,ChargeLevel=1000,ChargeReady=1000,ChargeRate=50}};
                    p[0].Par.Add("maxSide", 60);
                    p[1].Par.Add("maxSide", 60);
                    tank= AddTank(new string[] { "platformRed3", "towerRed3" },p);
                    v = new SelectNearestByAngle(friendly);
                    tank.Par.CopyPar (p[0]);
                    tank.Children[0].Par.CopyPar ( p[1]);
                    //добавить танк в списки союзников и игровых обьектов
                    // слежение за целью и выстрел для башни
                    tank.Children[0].AddBehavior(v, "SelectNearestByAngle");
                    tank.Children[0].AddBehavior(new RotateTo(v.currTarget), "RotateTo");
                    tank.Children[0].AddBehavior(new ShootWhenAimed(v.currTarget, "LightShell", friendly), "ShootWhenAimed");
                    tank.Children[0].AddBehavior(new Reloading(), "Reloading");
                    ////tank.Children[0].AddBehavior(new SynchronizeCoords(tank.Par), "SynchronizeCoords");
                    Map.ContainerSetLeftClickHandler(tank.Container, tank.Click);
                    tank.AddBehavior(new Patrol(5,true, new DoubleCoordinate(800,100),
                        new DoubleCoordinate(100,200),new DoubleCoordinate(500,500)),"Patrol");
                    enemies.Add(tank);
                    GameObjectsList.Add(tank);
                    
                    break;


            }
        }
        public void setMovementGoalByClick(int x, int y, int cx, int cy)
        {
           
            if (ClickedObj != null)
            {
                DoubleCoordinate c = new DoubleCoordinate(x, y);
                ClickedObj.RemoveBehavior("MoveForward");
                ClickedObj.RemoveBehavior("RotateTo");
                ClickedObj.RemoveBehavior("StopAtPoint");
                ClickedObj.AddBehavior(new MoveForward(), "MoveForward");
                ClickedObj.AddBehavior(new RotateTo(c), "RotateTo");
                var b = new StopAtPoint(c);
                ClickedObj.AddBehavior(b,"StopAtPoint");
                b.SetMinimalRange();
                
            }
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
                    u.SetAngle(-90);
                    u.Par.AngularVelocity = 1;
                    u.AddBehavior(new ControlSimpleFlyer(), "ctrl");
                    u.Par.ChargeLevel = 1000;
                    u.Par.ChargeReady = 1000;
                    u.Par.ChargeRate = 10;
                    u.AddBehavior(new ShootWhenAimed(Base,"Rocket",friendly), "Shoot");
                    u.AddBehavior(new Reloading(), "Reloadng");
                    enemies.Add(u);
                    
                    break;
                
                    
            }

            if (u != null)
            {
                GameObjectsList.Add(u);
                teamUnits[u.Team].Add(u);
            }
        }
        public void AddShell(string name,List<UGameObjectBase> targetList,int X,int Y,int Angle)
        {
            UGameObjectBase obj = null;
            switch (name)
            { 
                case "LightShell":
                     obj = new UGameObjectBase(X, Y, "blast5");
                    Map.ContainerSetMaxSide(obj.Container, 12);
                    obj.Par.Velocity = 12;
                    obj.AddBehavior(new MoveForward(), "MoveForward");
                    obj.SetAngle(Angle);
                    obj.AddBehavior(new hitAny(targetList),"hitAny");
                    

                    break;
                case "Rocket":
                    obj = new UGameObjectBase(X, Y, "MissileRed1", 1);
                    obj.SetContainerSize(30, 12);
                    obj.Par.Velocity = 8;
                    obj.SetAngle(Angle);
                    obj.AddBehavior(new MoveForward(), "Move");
                    obj.AddBehavior(new Hit(Base), "Hit");

                    break;

            }
            if (obj != null)
            {
                GameObjectsList.Add(obj);
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
