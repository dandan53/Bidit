using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Bidit.Models;

namespace Bidit.Controllers
{
    public class ReviewController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public bool Post(ReviewRequest request)
        {
            bool retVal = false;

            if (request != null)
            {
                var review = new ReviewData() {FromCID = request.FromCID, AboutCID = request.AboutCID, Feedback = request.Feedback, Rate = request.Rate};

                retVal = DAL.Instance.AddReview(review);
            }

            return retVal;
        }

        public IEnumerable<ReviewData> Get(int CID)
        {
            List<ReviewData> retVal = null;

            var reviews = DAL.Instance.GetReviewsByAboutCID(CID);
            if (reviews != null && reviews.Count > 0)
            {
                retVal = reviews.Values.ToList();
            }
            
            return retVal;
        }
            
    }

    public class ReviewRequest
    {
        public int FromCID { get; set; }
        public int AboutCID { get; set; }
        public decimal Rate { get; set; }
        public string Feedback { get; set; }
    }

    public class ReviewsResult
    {
        public int CID { get; set; }
        //public Dictionary<int, ReviewData> Reviews { get; set; }
        public decimal AverageRate { get; set; }
    }
}
