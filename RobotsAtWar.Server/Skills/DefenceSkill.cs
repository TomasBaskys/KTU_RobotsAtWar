using System;
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

        public DefenceOutcome Defend(int timeToDefend, WarriorState warriorState)
        {
            if (timeToDefend < 1)
            {
                return DefenceOutcome.WrongData;
            }
            _defeceTimer.Sleep(timeToDefend);

            warriorState.State = States.DoingNothing;

            return DefenceOutcome.Success;
        }

        public AttackOutcome GetAttacked(int damage, WarriorState warriorState, Warrior enemy)
        {
            var randomNumberGenerator = new Random();

            var finalDamage = damage;
            if (warriorState.State == States.Defending)
            {
                if (randomNumberGenerator.Next(_shield.MinDiceRoll, _shield.MaxDiceRoll) <= _shield.DiceRollBaseline)
                {
                    enemy.GetAttacked(damage);
                    return AttackOutcome.Reflected;
                }
                finalDamage = _shield.MitigateDamage(damage);
                warriorState.Life -= finalDamage;
                return AttackOutcome.Defended;
            }
            warriorState.Life -= finalDamage;

            if (warriorState.Life <= 0) warriorState.State = States.Dead;
            return AttackOutcome.Success;
        }
    }
}