using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Bidit.Models;

namespace Bidit.Controllers
{
    public class LoginController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public LoginResult Post(LoginRequest request)
        {
            var retVal = new LoginResult();

            if (request != null)
            {
                var user = DAL.Instance.GetUserByUsernameAndPassword(request.Username, request.Password);
                if (user != null && user.CID > 0)
                {
                    retVal.User = user;
                }
            }

            return retVal;
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLogout { get; set; }
    }

    public class LoginResult
    {
        public User User { get; set; }
    }
}
