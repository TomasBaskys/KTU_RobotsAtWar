using System;
using System.Net;
using System.Net.Mail;
using RobotsAtWar.Server.Tools;

namespace RobotsAtWar.Server
{
    public class RegistrationToSystem
    {
        public static void AddUser(string email, string freeText)
        {
            string newUserGuid = Guid.NewGuid().ToString();
            string userGuid = newUserGuid.ToString();
            Database.AddUser(email, freeText, userGuid);
            SendConfirmationLetter(email, userGuid);
        }

        private static void SendConfirmationLetter(string email, string newUserGuid)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();

            message.To.Add(email);
            message.From = new MailAddress("robotconfirmator@gmail.com", "RobotsAtWar");
            message.Subject = "RobotsAtWar game!";
            message.Body = "You have registered for RobotsAtWar game!\n\n" +
                           "Your RobotId is " + newUserGuid + "\n\n" +
                           "To complete registration, please follow this link: " +
                           ConfigSettings.ReadSetting("ServerUrl") + "registerusertosystem/" + newUserGuid;
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;

            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("robotconfirmator@gmail.com", "AdForm2015");

            try
            {
                Console.WriteLine("start to send email over SSL ...");
                client.Send(message);
                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(e.Message);
            }
        }

        public static string AddUserAsConfirmed(string userGuid)
        {
            try
            {
                Database.ConfirmUser(userGuid);
            }
            catch (Exception)
            {
                return "Failed to confirm user";
            }
            return "confirmed user " + userGuid;
        }
    }
}