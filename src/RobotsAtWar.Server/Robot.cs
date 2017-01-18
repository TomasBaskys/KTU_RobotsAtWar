using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RobotsAtWar.Server.Enums;
using Action = RobotsAtWar.Server.Enums.Action;

namespace RobotsAtWar.Server
{
    public class Robot
    {
        public Robot(string robotId, string robotName, string strategy)
        {
            RobotId = robotId;
            RobotName = robotName;

            RobotStrategy = new RealStrategy(strategy).DeserializeStrategy();
        }

        public Robot(dynamic row)
        {
            RobotId = row.RobotId;
            RobotName = row.RobotName;

            IStrategy strategy = row.Strategy != null
                ? (IStrategy)new RealStrategy(row.Strategy.ToString())
                : new NullStrategy();

            try
            {
                RobotStrategy = strategy.DeserializeStrategy();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"RobotStrategy must be defined before using 'Auto' battle mode.");
            }
        }

        public string RobotId { get; set; }

        public string RobotName { get; set; }

        public RobotStatus Status { get; set; }

        public bool Ready { get; set; }

        public Robot Enemy { get; set; }

        public PlayType PlayType { get; set; }

        public RobotStrategy RobotStrategy { get; set; }
    }

    public enum PlayType
    {
        Manual,
        Auto
    }

    public interface IStrategy
    {
        RobotStrategy DeserializeStrategy();
    }

    public class RealStrategy : IStrategy
    {
        private readonly string _strategy;

        public RealStrategy(string strategy)
        {
            _strategy = strategy;
        }

        public RobotStrategy DeserializeStrategy()
        {
            return JsonConvert.DeserializeObject<RobotStrategy>(_strategy);
        }
    }

    public class NullStrategy : IStrategy
    {
        public RobotStrategy DeserializeStrategy()
        {
            return new RobotStrategy { Strategy = new List<RobotTurn>() };
        }
    }

    public class RobotStrategy
    {
        public List<RobotTurn> Strategy { get; set; }
    }

    public class RobotTurn
    {
        public Action Action { get; set; }

        public ActionStrength Level { get; set; }
    }
}