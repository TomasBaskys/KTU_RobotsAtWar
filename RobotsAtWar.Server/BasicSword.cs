using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public interface IWeapon
    {
        Response Attack(Strength strength);
    }

    public class BasicSword : IWeapon
    {
        public Response Attack(Strength strength)
        {
            return Response.Success;
        }
    }
}
