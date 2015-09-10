using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bidit.Models
{
    public class UserSettings
    {
        public List<Item> BidList { get; set; }

        public List<Item> AskList { get; set; }

        public Dictionary<int, int> SubscribedProductIdDic { get; set; }
        
        //public bool IsEmailUpdates { get; set; }
    }
}
