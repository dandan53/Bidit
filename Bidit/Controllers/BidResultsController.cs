using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Bidit.Models;

namespace Bidit.Controllers
{
    public class BidResultsController : ApiController
    {
        // GET api/Todo/5
        public BidResultsResult Get(int id)
        {
            BidResultsResult results = null;

            Item item = DAL.Instance.GetItem(id);
            if (item != null)
            {
                results = new BidResultsResult() { Bid = item };
            }

            return results;
        }

    }

    public class BidResultsResult
    {
        public Item Bid { get; set; }
        public User FirstAsk { get; set; }
    }
}
