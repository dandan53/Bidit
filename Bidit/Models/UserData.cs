using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidit.Models
{
    public class UserData
    {
        public User User { get; set; }
        public Dictionary<int, Item> BidIdToBidDic { get; set; }
        public Dictionary<int, Item> AskIdToAskDic { get; set; }
        public Dictionary<int, int> SubscribedProductIdDic { get; set; }
    }
}