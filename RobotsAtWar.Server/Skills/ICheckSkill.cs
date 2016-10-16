namespace RobotsAtWar.Server.Skills
{
    public interface ICheckSkill
    {
        WarriorState Check(Warrior enemy);
    }
}