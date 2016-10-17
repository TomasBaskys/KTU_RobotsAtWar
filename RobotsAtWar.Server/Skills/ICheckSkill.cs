namespace RobotsAtWar.Server.Skills
{
    public interface ICheckSkill
    {
        RobotStatus Check(Robot enemy);
    }
}