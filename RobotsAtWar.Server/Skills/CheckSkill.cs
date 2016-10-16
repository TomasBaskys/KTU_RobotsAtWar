namespace RobotsAtWar.Server.Skills
{
    public class CheckSkill : ICheckSkill
    {
        public WarriorState Check(Warrior enemy)
        {
            return enemy.WarriorState;
        }
    }
}