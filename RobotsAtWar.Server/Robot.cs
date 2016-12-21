﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RobotsAtWar.Server.Enums;
using Action = RobotsAtWar.Server.Enums.Action;

namespace RobotsAtWar.Server
{
    public class Robot
    {
        public Robot(dynamic row, PlayType playType)
        {
            RobotId = row.RobotId;
            RobotName = row.RobotName;
            PlayType = playType;

            try
            {
                RobotStrategy = playType == PlayType.Auto
                    ? JsonConvert.DeserializeObject<RobotStrategy>(row.Strategy.ToString())
                    : null;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"Strategy must be defined before using 'Auto' battle mode.");
            }
        }

        public string RobotId { get; set; }

        public string RobotName { get; set; }

        public RobotState State { get; set; }

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

    public class RobotStrategy
    {
        public List<RobotTurn> Strategy { get; set; }
    }

    public class RobotTurn
    {
        public Action Action { get; set; }

        public ActionStrength? Level { get; set; }
    }
}