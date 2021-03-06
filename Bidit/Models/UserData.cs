﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidit.Models
{
    public class UserData
    {
        public User User { get; set; }
        public List<int> BidIdList { get; set; }
        public List<int> AskIdList { get; set; }
        public List<int> HistoryBidIdList { get; set; }
        public List<int> HistoryAskIdList { get; set; }
        public List<int> SubscribedProductIdList { get; set; }
        public List<int> ReviewIdAboutUserList { get; set; }
        public List<int> ReviewIdFromUserList { get; set; }
    }
}