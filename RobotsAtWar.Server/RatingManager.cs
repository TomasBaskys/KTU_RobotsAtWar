﻿using System;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public static class RatingManager
    {
        private const int KFactor = 30;
        private const int FFactor = 30;

        private static int CalculateNewRating(int warriorRating, int enemyRating, RobotState outcome)
        {
            if (outcome == RobotState.Winner)
            {
                return (int)(KFactor - KFactor * (1 / Math.Pow(10, (enemyRating - warriorRating) / FFactor + 1)));
            }
            return (int)(-KFactor * (1 / Math.Pow(10, (enemyRating - warriorRating) / FFactor + 1)));
        }

        public static void UpdateWarriorRating(Guid winnerGuid, Guid loserGuid)
        {
            var warrior = Database.GetRank(winnerGuid);
            var enemy = Database.GetRank(loserGuid);

            warrior += CalculateNewRating(warrior, enemy, RobotState.Winner);
            enemy += CalculateNewRating(enemy, warrior, RobotState.Dead);

            Database.SetRank(winnerGuid, warrior);
            Database.SetRank(loserGuid, enemy);
        }
    }
}