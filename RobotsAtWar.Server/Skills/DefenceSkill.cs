/*using System;
using System.Globalization;
using log4net.Repository.Hierarchy;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers.Defend;
using RobotsAtWar.Server.Shields;

namespace RobotsAtWar.Server.Skills
{
    public class DefenceSkill : IDefenceSkill
    {
        private readonly IDefenceTimer _defeceTimer;
        private readonly BaseShield _shield;

        public DefenceSkill(IDefenceTimer defeceTimer, BaseShield shield)
        {
            _defeceTimer = defeceTimer;
            _shield = shield;
        }

        public DefenceState Defend(int timeToDefend, RobotStatus robotStatus)
        {
            if (timeToDefend < 1)
            {
                return DefenceState.WrongData;
            }
            _defeceTimer.Sleep(timeToDefend);

            robotStatus.RobotState = RobotState.Idle;

            return DefenceState.Success;
        }

        public AttackResult GetAttacked(int damage, RobotStatus robotStatus, Robot enemy)
        {
            var randomNumberGenerator = new Random();

            var finalDamage = damage;
            if (robotStatus.RobotState == RobotState.Defending)
            {
                if (randomNumberGenerator.Next(_shield.MinDiceRoll, _shield.MaxDiceRoll) <= _shield.DiceRollBaseline)
                {
                    enemy.GetAttacked(damage);
                    return AttackResult.Reflected;
                }
                finalDamage = _shield.MitigateDamage(damage);
                robotStatus.Life -= finalDamage;
                return AttackResult.Defended;
            }
            robotStatus.Life -= finalDamage;

            if (robotStatus.Life <= 0) robotStatus.RobotState = RobotState.Dead;
            return AttackResult.Success;
        }
    }
}*/