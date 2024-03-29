﻿using System;
using System.Collections.Generic;
using System.Linq;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.Readers;
using RobotsAtWar.Server.Writers;

namespace RobotsAtWar.Server
{
    public class BattleFields
    {
        public static void LoadBattleField()
        {
            var battles = GetBattleFields();

            foreach (var battleField in battles)
            {
                var battle = new BattleField(
                    battleField.HostRobotId, 
                    battleField.BattleName, 
                    battleField.BattleType,
                    battleField.BattleId);

                BattleFieldSingleton.Instance.BattleFields.Add(battle);
            }
        }

        public static string CreateBattleField(string hostId, string battleFieldName, RoomType roomType)
        {
            var battle = new BattleField(hostId, battleFieldName, roomType);
            BattleFieldSingleton.Instance.BattleFields.Add(battle);

            var battleWriter = new BattleWriter();
            battleWriter.AddBattle(battle);

            return battle.BattleId;
        }

        public static void JoinBattleField(string battleFieldId, string robotId, PlayType playType)
        {
            BattleField battleField = GetBattleField(battleFieldId);

            battleField.RegisterRobot(robotId, playType);

            if (battleField.Robots.Count == 2)
            {
                battleField.StartBattle();
            }
        }

        public static IEnumerable<BattleField> GetBattleFields()
        {
            var battleReader = new BattleReader();
            var activeBattles = battleReader.GetActiveBattles();

            return activeBattles.Where(p => p.BattleType == RoomType.Public);
        }

        public static BattleField GetBattleField(string battleFieldId)
        {
            BattleField battleField = BattleFieldSingleton.Instance.BattleFields.FirstOrDefault(r => r.BattleId == battleFieldId);
            
            if (battleField == null)
            {
                throw new ArgumentException($"Battlefield with id: '{battleFieldId}' does not exist.");
            }

            return battleField;
        }

        public static string RobotStatus(string battleFieldId, string robotId)
        {
            BattleField battle = GetBattleField(battleFieldId);

            Robot robot = battle.GetRobot(robotId);
            RobotStatus status = robot.Status;

            if (status.RobotState == RobotState.Interrupted
                && status.LastReceivedDamage > 0)
            {
                var damage = status.LastReceivedDamage.ToString();

                status.LastReceivedDamage = 0;

                return damage;
            }
            if (status.RobotState == RobotState.Dead)
            {
                battle.BattleState = BattleState.Finished;
                return RobotState.Dead.ToString();
            }
            if (robot.Enemy.Status.RobotState == RobotState.Dead)
            {
                battle.BattleState = BattleState.Finished;
            }

            return null;
        }

        public static RobotsLifePoints BattleStatus(string battleFieldId, string robotId)
        {
            BattleField battle = GetBattleField(battleFieldId);

            Robot robot = battle.GetRobot(robotId);

            return new RobotsLifePoints
            {
                Robot = robot.Status.Life > 0 ? robot.Status.Life : 0,
                Enemy = robot.Enemy.Status.Life > 0 ? robot.Enemy.Status.Life : 0
            };
        }
    }

    public class RobotsLifePoints
    {
        public int Robot { get; set; }
        public int Enemy { get; set; }
    }

    public class BattleFieldSingleton
    {
        public readonly List<BattleField> BattleFields;

        private static BattleFieldSingleton _instance;

        private BattleFieldSingleton()
        {
            BattleFields = new List<BattleField>();
        }

        public static BattleFieldSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleFieldSingleton();
                }

                return _instance;
            }
        }
    }
}
