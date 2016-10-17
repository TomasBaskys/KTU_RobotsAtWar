/*using log4net;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers.Atack;
using RobotsAtWar.Server.Weapons;

namespace RobotsAtWar.Server.Skills
{
    public class AttackSkill : IAttackSkill
    {
        private readonly IAttackTimer _weaponTimer;
        private readonly IWeapon _weapon;

        public AttackSkill(IAttackTimer weaponTimer, IWeapon weapon)
        {
            _weaponTimer = weaponTimer;
            _weapon = weapon;
        }

        public AttackResult Attack(Strength strength,WarriorState warriorState, Warrior enemy)
        {
            int damage = _weapon.CalculateAttackDamage(strength);
            if (damage == 0)
            {
                return AttackResult.WrongData;
            }

            _weaponTimer.Sleep(strength);

            if (warriorState.State == States.Interrupted)
            {
                return AttackResult.Interrupted;
            }
            

            var attackResponse = enemy.GetAttacked(damage);

            if (attackResponse == AttackResult.Reflected)
            {
                warriorState.State = States.Stunned;
                _weaponTimer.Stun(strength);
            }
            warriorState.State = States.DoingNothing;

            return attackResponse;
        }

    }
}*/