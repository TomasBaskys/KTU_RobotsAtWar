using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Shields
{
    public interface IShield
    {
        int MitigateDamage(int damage);
    }
}