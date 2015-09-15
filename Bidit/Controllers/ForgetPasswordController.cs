using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bidit.Controllers
{
    public class ForgetPasswordController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public ForgetPasswordResult Post(ForgetPasswordRequest request)
        {
            var retVal = new ForgetPasswordResult();

            if (request != null)
            {
                var user = DAL.Instance.GetUserByEmail(request.Email);
                if (user != null)
                {
                    const string subject = "שחזור סיסמה באתר בידיט";
                    string body = EmailTemplate.ForgetPasswordEmail;
                    
                    retVal.IsSuccess = EmailSender.SendMail(subject, body, user);
                }
            }

            return retVal;
        }
    }

    public class ForgetPasswordRequest
    {
        public string Email { get; set; }
    }

    public class ForgetPasswordResult
    {
        public bool IsSuccess { get; set; }
    }
}