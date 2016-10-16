using System;
using System.Net.Mail;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    /// <summary>
    /// blablabla
    /// </summary>
    public class RequestToSystemRegistrationController : ApiController
    {
        /// <summary>
        /// Method attempts to register a new user with a given EMail and some free text. It sends an email with a generated user GUID to the given EMail address.
        /// User must open a link given in the EMail to confirm a registration.
        /// </summary>
        /// <param name="email">
        /// EMail of the user.
        /// </param>
        /// <param name="freeText">
        /// Free form text of the user.
        /// </param>
        /// <returns>
        /// String which contains a message if registration were successful.
        /// </returns>
        [Route("RequestToSystemRegistration/{email}/{freeText}")]
        public string Post(string email, string freeText)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                RegistrationToSystem.AddUser(email, freeText);
                return "Please check your inbox";
            }
            catch (FormatException)
            {
                return "Email address is incorrect!!";
            }

        }
    }
}