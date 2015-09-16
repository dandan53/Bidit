using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Bidit.Models;

namespace Bidit.Controllers
{
    public class RegisterController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public RegisterResult Post(RegisterRequest request)
        {
            var retVal = new RegisterResult();

            if (request != null)
            {
                var user = DAL.Instance.AddUser(request);
                if (user != null && user.CID > 0)
                {
                    retVal.User = user;

                    SendEmail(user);
                }
            }

            return retVal;
        }

        private void SendEmail(User user)
        {
            try
            {
                string mailSubject = "הרישום שלך לבידיט התבצע בהצלחה";
                string mailBody = EmailTemplate.RegistrationEmail;

                // Sending an email
                var isMailSent = EmailSender.SendMail(mailSubject, mailBody, user);
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class RegisterResult
    {
        public User User { get; set; }
    }
}
