using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidit.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DueDate { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public string Product { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal FirstPrice { get; set; }
        public decimal SecondPrice { get; set; }
        public decimal ThirdPrice { get; set; }
        public bool IsBidEnded { get; set; }


        public User BidUser { get; set; }
        public User FirstAskUser { get; set; }
        public User SecondAskUser { get; set; }
        public User ThirdAskUser { get; set; }

        public int BidCID { get; set; }
        public int FirstAskCID { get; set; }
        public int SecondAskCID { get; set; }
        public int ThirdAskCID { get; set; }

        public decimal NewPrice { get; set; }
        public int NewAskCID { get; set; }
        public string FirstPriceDisplay { get; set; }
    }
}