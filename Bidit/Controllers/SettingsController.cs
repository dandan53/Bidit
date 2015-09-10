using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Bidit.Models;

namespace Bidit.Controllers
{
    public class SettingsController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public SettingsResult Post(SettingsRequest request)
        {
            var retVal = new SettingsResult();

            retVal.IsSuccess = DAL.Instance.SaveUserSettings(request);
            
            return retVal;
        }
    }

    public class SettingsRequest
    {
        public int CID { get; set; }
        public bool IsEmailUpdates { get; set; }
        public Dictionary<int, int> SubscribedProductIdDic { get; set; }
    }

    public class SettingsResult
    {
        public bool IsSuccess { get; set; }
    }
}
