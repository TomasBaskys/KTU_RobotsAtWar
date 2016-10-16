using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    /// <summary>
    /// Controller is used to confirm a user
    /// </summary>
    public class RegisterUserToSystemController : ApiController
    {
        /// <summary>
        /// Method attempts to confirm a registration of the user which GUID is passed as a parameter.
        /// </summary>
        /// <param name="myGuid">
        /// GUID of the user which registartion will be confirmed.
        /// </param>
        /// <returns>
        /// String which contains a message if confirmation were successful.
        /// </returns>
        public string Get(Guid myGuid)
        {
            return RegistrationToSystem.AddUserAsConfirmed(myGuid);
        }

    }
}