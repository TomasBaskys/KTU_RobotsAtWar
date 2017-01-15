using System.Web.Http;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.Readers;
using RobotsAtWar.Server.Writers;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class StrategyController : ApiController
    {
        [HttpGet]
        public RobotStrategy Get(string robotId)
        {
            var reader = new RobotReader();
            Robot robot = reader.GetRobotInfo(robotId);

            return robot.RobotStrategy;
        }

        [HttpGet]
        public void Update(string robotId, Action action, ActionStrength level)
        {
            var reader = new RobotReader();
            Robot robot = reader.GetRobotInfo(robotId);

            robot.RobotStrategy.Strategy.Add(new RobotTurn { Action = action, Level = level });

            var writer = new StrategyWriter();
            writer.UpdateStrategy(robot.RobotId, robot.RobotStrategy);
        }

        [HttpGet]
        public void Remove(string robotId)
        {
            var reader = new RobotReader();
            Robot robot = reader.GetRobotInfo(robotId);

            robot.RobotStrategy.Strategy.RemoveAt(robot.RobotStrategy.Strategy.Count-1);

            var writer = new StrategyWriter();
            writer.UpdateStrategy(robot.RobotId, robot.RobotStrategy);
        }
    }
}

#pragma warning restore 1591
