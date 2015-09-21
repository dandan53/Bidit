using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidit.Models
{
    public class ReviewData
    {
        public int Id { get; set; }
        public int FromCID { get; set; }
        public int AboutCID { get; set; }
        public decimal Rate { get; set; }
        public string Feedback { get; set; }
    }
}