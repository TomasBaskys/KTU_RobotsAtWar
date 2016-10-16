using System;
using log4net;
using log4net.Repository.Hierarchy;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers.Atack;
using RobotsAtWar.Server.MoveTimers.Defend;
using RobotsAtWar.Server.Skills;
using RobotsAtWar.Server.Weapons;

namespace RobotsAtWar.Server
{
    public class Warrior
    {
        private Guid Guid { get; set; }
        private readonly IAttackSkill _attackSkill;
        private readonly IDefenceSkill _defenceSkill;
        private readonly ICheckSkill _checkSkill;
        private readonly IRestSkill _restSkill;
        public WarriorState WarriorState { get; private set; }
        public Warrior Enemy { get; set; }

        public Warrior(Guid guid, IAttackSkill attackSkill, IDefenceSkill defendSkill, ICheckSkill checkSkill, IRestSkill restSkill)
        {
            WarriorState = new WarriorState {Life = 10, State = States.DoingNothing};
            Guid = guid;
            _attackSkill = attackSkill;
            _defenceSkill = defendSkill;
            _checkSkill = checkSkill;
            _restSkill = restSkill;
            
        }

        public AttackOutcome Attack(Strength strength)
        {
            WarriorState.State = States.Attacking;
            AttackOutcome attackOutcome = AttackOutcome.Defended;
            if(WarriorState.State != States.Dead)
            {
                attackOutcome = _attackSkill.Attack(strength, WarriorState, Enemy);
            }
            WarriorState.State = States.DoingNothing;
            return attackOutcome;
        }

        public WarriorState Check()
        {
            WarriorState.State = States.Checking;
            var enemyState = _checkSkill.Check(Enemy);
            WarriorState.State = States.DoingNothing;
            return enemyState;
        }

        public AttackOutcome GetAttacked(int damage)
        {

            if( WarriorState.State == States.Resting || WarriorState.State == States.Attacking)
                WarriorState.State = States.Interrupted;

            var outcome = _defenceSkill.GetAttacked(damage, WarriorState, Enemy);

            return outcome;
        }

        public DefenceOutcome Defend(int timeToDefend)
        {
            WarriorState.State = States.Defending;

            var defenceOutcome = _defenceSkill.Defend(timeToDefend,WarriorState);

            WarriorState.State = States.DoingNothing;
            return defenceOutcome;
        }

        public RestOutcome Rest(int time)
        {            
            WarriorState.State = States.Resting;

            var restResponse = _restSkill.Rest(time, WarriorState);

            WarriorState.State = States.DoingNothing;

            return restResponse;
        }

        public bool IsAlive()
        {
            return WarriorState.Life > 0;
        }
    }
}