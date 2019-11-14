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
        Random r=new Random();
        public UniversalMap_Wpf Map;
        public List<UGameObjectBase> GameObjectsList = new List<UGameObjectBase>();
        public TimerController timer = new TimerController();
        List<List<UGameObjectBase>> teamUnits = new List<List<UGameObjectBase>>();
        public List<UGameObjectBase> friendly = new List<UGameObjectBase>();
        public List<UGameObjectBase> enemies = new List<UGameObjectBase>();
        //List<UGameObjectBase> bullets = new List<UGameObjectBase>();
        //List<UGameObjectBase> enemyBullets = new List<UGameObjectBase>();
        public UGameObjectBase ClickedObj;
        public UGameObjectBase RightClickedObj;
        public int totalResources=1000;
        public int ClickTimeCount;
        public UGameObjectBase Base;
        public Dictionary<string, int> PriceList = new Dictionary<string, int>();
        public Wave[] tierList = new Wave[5];
        public int WaveNumber;
        public int WaveCountdown = 300;
        public int SpawnCountdown=99999999;
        public Game(int teams)
        {
            for(int i = 0; i < teams; i++)
            {
                teamUnits.Add(new List<UGameObjectBase>());
            }
            timer.AddAction(EnemiesAct, 10);
            PriceList.Add("scavenger", 50);
            PriceList.Add("allyLightTank", 100);
            PriceList.Add("allyMediumTank", 250);
            PriceList.Add("Baneblade", 700);
            PriceList.Add("enemyLightTank", 100);

            tierList[0]=new Wave();
            tierList[0].unitList.Add("enemyLightTank");
            tierList[0].Resources = 1000;
            tierList[0].AvgSpawnInterval = 300;
            tierList[1] = new Wave();
            tierList[1].unitList.Add("enemyLightTank");
            tierList[1].Resources = 1000;
            tierList[1].AvgSpawnInterval = 300;



        }
        void Spawn()
        {
            int i;
            int count=0;
            while (true)
            {
                i = r.Next(0, tierList[WaveNumber].unitList.Count);
                if (PriceList[tierList[WaveNumber].unitList[i]] < tierList[WaveNumber].Resources)
                {
                    int sideID=r.Next(1, 5);
                    int coord;
                    switch (sideID)
                    {
                        case 1:
                            //Left
                            coord = r.Next(0, Map.YAbsolute);
                            CreateTank(tierList[WaveNumber].unitList[i], 0, coord);
                            break;
                        case 2:
                            //Right
                            coord = r.Next(0, Map.YAbsolute);
                            CreateTank(tierList[WaveNumber].unitList[i], Map.XAbsolute, coord);
                            break;
                        case 3:
                            //Up
                            coord = r.Next(0, Map.XAbsolute);
                            CreateTank(tierList[WaveNumber].unitList[i], coord, 0);
                            break;
                        case 4:
                            //Down
                            coord = r.Next(0, Map.XAbsolute);
                            CreateTank(tierList[WaveNumber].unitList[i], coord, Map.YAbsolute);
                            break;


                    }

                    tierList[WaveNumber].Resources -= PriceList[tierList[WaveNumber].unitList[i]];
                    SpawnCountdown = tierList[WaveNumber].AvgSpawnInterval + r.Next(-50, 50);
                    break;
                }
                else
                {
                    count++;
                    if (count > 50)
                    {
                        SpawnCountdown = 9999999;
                        WaveCountdown = 1500;
                        WaveNumber++;
                        break;
                    }
                }
            }
        }
        void EnemiesAct()
        {
            ClickTimeCount--;
            WaveCountdown--;
            if (WaveCountdown == 0)
            {

                SpawnCountdown = tierList[WaveNumber].AvgSpawnInterval + r.Next(-50, 50);
            }
            SpawnCountdown--;
            if (SpawnCountdown == 0)
            {
                Spawn();
            }
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
            Base.Par.HP = 99999;
        }
        UCompositeGameObject AddTank(string[] picList, GOParams[] par)
        {
            UCompositeGameObject tank = new UCompositeGameObject(par[0].X, par[0].Y, picList[0]);
            Map.ContainerSetMaxSide(tank.Container,(int)par[0].Par["maxSide"]);
            tank.AddChild(0, 0, 0, picList[1]);

            Map.ContainerSetMaxSide(tank.Children[0].Container, (int)par[1].Par["maxSide"]);
            return tank;
        }
        UCompositeGameObject AddBaneBlade(string[] picList, GOParams[] par)
        {
            UCompositeGameObject tank = new UCompositeGameObject(par[0].X, par[0].Y, picList[0]);
            Map.ContainerSetMaxSide(tank.Container, (int)par[0].Par["maxSide"]);
           
            tank.AddChild(50, 40, 0, picList[1]);
            tank.AddChild(50, -40, 0, picList[1]);
            tank.AddChild(-50, 40, 0, picList[1]);
            tank.AddChild(-50, -40, 0, picList[1]);
            tank.AddChild(0, 0, 0, picList[2]);
            for (int i = 0; i < 5; i++)
            {
                Map.ContainerSetMaxSide(tank.Children[i].Container, (int)par[i+1].Par["maxSide"]);
            }

            
            return tank;
        }
        public void CreateTank(string tankName, int x, int y, int angle=0)
        {
            UCompositeGameObject tank;
            GOParams[] p;
            SelectNearestByAngle v;
            switch (tankName)
            { 
                    
                    
                case "scavenger":
                        p=new GOParams []{
                        new GOParams {X=x,Y=y,Velocity=2,AngularVelocity=3},
                        new GOParams{X=x,Y=y,AngularVelocity=1.5,ChargeLevel=1000,ChargeReady=1000,ChargeRate=3}};
                    p[0].Par.Add("maxSide", 60);
                    p[0].Mass = 75;
                    p[1].Par.Add("maxSide", 60);
                    p[0].Type = UnitTypes.ground;
                    tank= AddTank(new string[] { "platformSand9", "flyerSand4"},p);
                    tank.AddBehavior(new ImpactControl(), "ImpactControl");
                    tank.Par.CopyPar (p[0]);
                    tank.Children[0].Par.CopyPar ( p[1]);
                    tank.Par.HP = 100;
                    tank.Par.Resources = 100;
                    tank.clicked = new storeLeftClick();
                    SelectNearestByRange s=new SelectNearestByRange(GameObjectsList);
                    tank.Children[0].AddBehavior(s, "SelectNearestByRange");
                    tank.AddBehavior(new CollectResources(s), "CollectResources");
                    Map.ContainerSetLeftClickHandler(tank.Container,ClickType.Left, tank.Click);
                    Map.ContainerSetLeftClickHandler(tank.Children[0].Container, ClickType.Left, tank.Click);

                    friendly.Add(tank);
                    GameObjectsList.Add(tank);
                    break;
                case "allyLightTank":
                        p=new GOParams []{
                        new GOParams {X=x,Y=y,Velocity=1.75,AngularVelocity=1},
                        new GOParams{X=x,Y=y,AngularVelocity=1.5,ChargeLevel=1000,ChargeReady=1000,ChargeRate=200}};
                    p[0].Par.Add("maxSide", 60);
                    p[0].Type = UnitTypes.ground;
                    p[0].Mass = 100;
                    p[1].Par.Add("maxSide", 60);
                    p[1].Range = 400;
                    tank= AddTank(new string[] { "platformSand1", "towerSand3" },p);
                    tank.AddBehavior(new ImpactControl(), "ImpactControl");
                    v = new SelectNearestByAngle(enemies);
                    tank.Par.CopyPar (p[0]);
                    tank.Children[0].Par.CopyPar ( p[1]);
                    tank.Par.HP = 100;
                    tank.Par.Resources = 100;
                    tank.Children[0].Par.ParString.Add("ShellName", "LightShell");
                       //добавить танк в списки союзников и игровых обьектов
                       // слежение за целью и выстрел для башни
                    tank.Children[0].AddBehavior(v, "SelectNearestByAngle");
                    tank.Children[0].AddBehavior(new RotateTo(v,1), "RotateTo");
                    tank.Children[0].AddBehavior(new ShootWhenAimed(v,"LightShell",enemies), "ShootWhenAimed");
                    tank.Children[0].AddBehavior(new Reloading(), "Reloading");
                    //tank.Children[0].AddBehavior(new SynchronizeCoords(tank.Par), "SynchronizeCoords");
                    tank.clicked = new storeLeftClick();
                    Map.ContainerSetLeftClickHandler(tank.Container,ClickType.Left, tank.Click);
                    Map.ContainerSetLeftClickHandler(tank.Children[0].Container, ClickType.Left, tank.Click);
                    friendly.Add(tank);
                    GameObjectsList.Add(tank);
                    break;
                case "allyMediumTank":
                        p=new GOParams []{
                        new GOParams {X=x,Y=y,Velocity=0.8,AngularVelocity=0.8},
                        new GOParams{X=x,Y=y,AngularVelocity=1.1,ChargeLevel=1200,ChargeReady=1200,ChargeRate=10}};
                    p[0].Par.Add("maxSide", 80);
                    p[0].Type = UnitTypes.ground;
                    p[0].Mass = 200;
                    p[1].Par.Add("maxSide", 80);
                    tank= AddTank(new string[] { "platformSand3", "towerSand4" },p);
                    v = new SelectNearestByAngle(enemies);
                    tank.Children[0].Par.ParString.Add("ShellName", "ArmorPiercing");
                    p[1].Range = 500;
                    tank.Par.CopyPar ( p[0]);
                    tank.Children[0].Par.CopyPar ( p[1]);
                    tank.AddBehavior(new ImpactControl(), "ImpactControl");
                    tank.Par.HP = 500;
                    tank.Par.Resources = 200;
                       //добавить танк в списки союзников и игровых обьектов
                       // слежение за целью и выстрел для башни
                    tank.Children[0].AddBehavior(v, "SelectNearestByAngle");
                    tank.Children[0].AddBehavior(new RotateTo(v,1), "RotateTo");
                    tank.Children[0].AddBehavior(new ShootWhenAimed(v,"ArmorPiercing",enemies), "ShootWhenAimed");
                    tank.Children[0].AddBehavior(new Reloading(), "Reloading");
                    //tank.Children[0].AddBehavior(new SynchronizeCoords(tank.Par), "SynchronizeCoords");
                    tank.clicked = new storeLeftClick();
                    Map.ContainerSetLeftClickHandler(tank.Container, ClickType.Left, tank.Click);
                    Map.ContainerSetLeftClickHandler(tank.Children[0].Container, ClickType.Left, tank.Click);
                    friendly.Add(tank);
                    GameObjectsList.Add(tank);
                    break;
                case "Baneblade":
                    {
                        p = new GOParams[]{
                        new GOParams {X=x,Y=y,Velocity=0.8,AngularVelocity=0.8},
                        new GOParams{X=x,Y=y,AngularVelocity=1.1,ChargeLevel=1200,ChargeReady=1200,ChargeRate=15,Range=500},
                        new GOParams{X=x,Y=y,AngularVelocity=1.1,ChargeLevel=1200,ChargeReady=1200,ChargeRate=15,Range=500},
                        new GOParams{X=x,Y=y,AngularVelocity=1.1,ChargeLevel=1200,ChargeReady=1200,ChargeRate=15,Range=500},
                        new GOParams{X=x,Y=y,AngularVelocity=1.1,ChargeLevel=1200,ChargeReady=1200,ChargeRate=15,Range=500},
                        new GOParams{X=x,Y=y,AngularVelocity=1,ChargeLevel=1200,ChargeReady=1200,ChargeRate=7,Range=750}};
                        p[0].HP = 5000;
                        p[0].Mass = 99999;
                        p[0].Par.Add("maxSide", 200);
                        p[1].Par.Add("maxSide", 120);
                        p[2].Par.Add("maxSide", 120);
                        p[3].Par.Add("maxSide", 120);
                        p[4].Par.Add("maxSide", 120);
                        p[5].Par.Add("maxSide", 200);
                        tank = AddBaneBlade(new string[] { "platformSand6", "towerSand4", "towerSand5" }, p);
                        tank.Par.CopyPar(p[0]);
                        p[0].Type = UnitTypes.ground;
                        tank.clicked = new storeLeftClick();
                        tank.Par.Resources = 1000;
                        for (int i = 0; i < 4; i++)
                        {
                            v = new SelectNearestByAngle(enemies);
                            tank.Children[i].Par.CopyParWithoutPosition(p[i+1]);
                            tank.Children[i].AddBehavior(v, "SelectNearestByAngle");
                            tank.Children[i].AddBehavior(new RotateTo(v,1), "RotateTo");
                            tank.Children[i].AddBehavior(new ShootWhenAimed(v, "ArmorPiercing", enemies), "ShootWhenAimed");
                            tank.Children[i].AddBehavior(new Reloading(), "Reloading");
                            Map.ContainerSetLeftClickHandler(tank.Children[i].Container, ClickType.Left, tank.Click);
                        }
                        tank.AddBehavior(new ImpactControl(), "ImpactControl");
                        v = new SelectNearestByAngle(enemies);
                        tank.Children[4].Par.CopyParWithoutPosition(p[5]);
                        tank.Children[4].AddBehavior(v, "SelectNearestByAngle");
                        tank.Children[4].AddBehavior(new RotateTo(v,1), "RotateTo");
                        tank.Children[4].AddBehavior(new ShootWhenAimed(v, "Ripper", enemies), "ShootWhenAimed");
                        tank.Children[4].AddBehavior(new Reloading(), "Reloading");
                        Map.ContainerSetLeftClickHandler(tank.Children[4].Container, ClickType.Left, tank.Click);
                        Map.ContainerSetLeftClickHandler(tank.Container, ClickType.Left, tank.Click);
                        friendly.Add(tank);
                        GameObjectsList.Add(tank);
                        break;
                    }

                case "enemyLightTank":
                     p=new GOParams []{
                        new GOParams {X=x,Y=y,Velocity=1,AngularVelocity=1},
                        new GOParams{X=x,Y=y,AngularVelocity=3.5,ChargeLevel=1000,ChargeReady=1000,ChargeRate= 100}};
                    p[0].Par.Add("maxSide", 60);
                    p[0].Mass = 100;
                    p[1].Par.Add("maxSide", 90);
                    p[1].Range = 400;
                    p[0].Type = UnitTypes.ground;
                    p[0].HP = 100;
                    tank= AddTank(new string[] { "platformRed3", "towerRed3" },p);
                    v = new SelectNearestByAngle(friendly);
                    tank.Par.CopyPar (p[0]);
                    tank.Children[0].Par.CopyPar ( p[1]);
                    tank.Par.Resources = 100;
                    tank.destroyedPictureName = "platformRedDestroyed3";
                    //добавить танк в списки союзников и игровых обьектов
                    // слежение за целью и выстрел для башни
                    tank.AddBehavior(new EnemyLightTankAI(friendly), "EnemyLightTankAI");
                    tank.AddBehavior(new ImpactControl(),"ImpactControl");
                    //tank.Children[0].AddBehavior(v, "SelectNearestByAngle");
                    //tank.Children[0].AddBehavior(new RotateTo(v), "RotateTo");
                    //tank.Children[0].AddBehavior(new ShootWhenAimed(v, "LightShell", friendly), "ShootWhenAimed");
                    tank.Children[0].AddBehavior(new Reloading(), "Reloading");

                    ////tank.Children[0].AddBehavior(new SynchronizeCoords(tank.Par), "SynchronizeCoords");
                    Map.ContainerSetLeftClickHandler(tank.Container, ClickType.Right, tank.RightClick);
                    Map.ContainerSetLeftClickHandler(tank.Children[0].Container, ClickType.Right, tank.RightClick);
                    //tank.AddBehavior(new Patrol(5, true, new DoubleCoordinate(800, 100),
                      //  new DoubleCoordinate(100, 200), new DoubleCoordinate(500, 500)), "Patrol");
                    //tank.AddBehavior(new Patrol(5, true, new DoubleCoordinate(100, 100),
                    //    new DoubleCoordinate(100, 200), new DoubleCoordinate(500, 500)), "Patrol");
                    enemies.Add(tank);
                    GameObjectsList.Add(tank);
                    
                    break;


            }
        }
        public void setMovementGoalByClick(int x, int y, int cx, int cy)
        {
           
            if (ClickTimeCount<=0&&ClickedObj != null&&ClickedObj.Par.HP>0)
            {
                DoubleCoordinate c = new DoubleCoordinate(x, y);
                ClickedObj.RemoveBehavior("MoveForward");
                ClickedObj.RemoveBehavior("RotateTo");
                ClickedObj.RemoveBehavior("StopAtPoint");
                ClickedObj.AddBehavior(new MoveForward(), "MoveForward");
                ClickedObj.AddBehavior(new RotateTo(c,0.75), "RotateTo");
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
        public void AddShell(string name,List<UGameObjectBase> targetList,int X,int Y,int Angle,int Range)
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
                    obj.Par.DamageMax = 5;

                    break;
                case "ArmorPiercing":
                    obj = new UGameObjectBase(X, Y, "blast5");
                    Map.ContainerSetMaxSide(obj.Container, 17);
                    obj.Par.Velocity = 12;
                    obj.AddBehavior(new MoveForward(), "MoveForward");
                    obj.SetAngle(Angle);
                    obj.AddBehavior(new hitAny(targetList),"hitAny");
                    obj.Par.DamageMax = 50;
                    break;
                case "Ripper":
                     obj = new UGameObjectBase(X, Y, "blast5");
                    Map.ContainerSetMaxSide(obj.Container, 25);
                    obj.Par.Velocity = 12;
                    obj.AddBehavior(new MoveForward(), "MoveForward");
                    obj.SetAngle(Angle);
                    obj.AddBehavior(new hitAny(targetList),"hitAny");
                    obj.Par.DamageMax = 200;
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
                obj.Par.Par.Add("Distance", 0);
                obj.Par.Range = Range;
                obj.Par.Type = UnitTypes.shell;
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
