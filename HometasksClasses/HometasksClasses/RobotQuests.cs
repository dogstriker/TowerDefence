using System.Windows;
using GameMaps;
using GameMaps.Layouts;

namespace HometasksClasses
{
    static public class RobotQuests
    {
        static public RobotQuestMap GetQuest(Window w, string name)
        {
            var map = MapCreator.GetUniversalMap(w, GetMapParams(name));
            map.DrawGrid();
            var lay = LayoutsFactory.GetLayout(LayoutType.SingleZone, w.Content);
            lay.Attach(map, 0);


            RobotQuestMap qMap = null;
            RobotMap rMap;
            SimpleRobot rBot;
            Coordinate c;
            switch(name)
            {
                case "task1":
                    rMap = new RobotMap(map);
                    rMap.SetExit(ExitPlacement.Fixed);
                    c = rMap.SetRobot(RobotPlacement.Random);
                    rMap.PlaceObstacles(ObstaclesFormation.SeparatedBlocks);
                    rBot = new SimpleRobot(map, rMap, c);
                    qMap = new RobotQuestMap(map, rMap, rBot);
                    break;
                case "task2":
                    rMap = new RobotMap(map);
                    rMap.SetExit(ExitPlacement.RandomCorner);
                    c = rMap.SetRobot(RobotPlacement.Random);
                    rMap.PlaceObstacles(ObstaclesFormation.SeparatedBlocks);
                    rBot = new SimpleRobot(map, rMap, c);
                    qMap = new RobotQuestMap(map, rMap, rBot);
                    break;
            }
            return qMap;
        }



        static private CellMapInfo GetMapParams(string name)
        {
            CellMapInfo cm;
            switch(name)
            {
                case "task1":
                    cm = new CellMapInfo(22, 22, 25, 10);
                    break;
                default:
                    cm = new CellMapInfo(20, 20, 25, 10);
                    break;
            }
            return cm;
        }
    }

}
