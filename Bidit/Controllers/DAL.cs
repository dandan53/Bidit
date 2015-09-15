using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using Bidit.Models;

namespace Bidit.Controllers
{
    public sealed class DAL
    {
        private Dictionary<int, UserData> CIDToUserDataDic = new Dictionary<int, UserData>();
        
        private new Dictionary<int, List<int>> ProductIdToSubscriberCIDList = new Dictionary<int, List<int>>();

        private Dictionary<int, Item> ItemIdToItemDic = new Dictionary<int, Item>();
        
        private static DAL instance = null;

        private DAL()
        {
        }

        public static DAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL();

                    //Init();
                }
                return instance;
            }
        }

        /// <summary>
        /// Items
        /// </summary>
        
        

        public Dictionary<int, Item> GetItems()
        {
            return ItemIdToItemDic;
        }

        public void AddItem(Item item)
        {
             User user = GetUserByCID(item.BidCID);
            if (user != null)
            {
                item.Id = CreateItemId();
                item.BidCID = user.CID;
                item.FirstPriceDisplay = "--";
                ItemIdToItemDic.Add(item.Id, item);

                NotifiySubscribers(item.ProductId, item);
            }
        }

        public void NotifiySubscribers(int productId, Item item)
        {
            if (ProductIdToSubscriberCIDList != null && ProductIdToSubscriberCIDList.ContainsKey(productId))
            {
                var CIDList = ProductIdToSubscriberCIDList[productId];
                foreach (var CID in CIDList)
                {
                    var user = GetUserByCID(CID);
                    if (user != null)
                    {
                        SendEmail(user, productId, item);
                    }
                }
            }
        }

        void SendEmail(User user, int productId, Item item)
        {
            try
            {
                string mailSubject = "מכרז חדש נפתח באתר בידיט";
                string mailBody = EmailTemplate.BidStartedEmail;
                
                // Sending an email
                var isMailSent = EmailSender.SendMail(mailSubject, mailBody, user, item);
            }
            catch (Exception ex)
            {
            }
        }


        public void UpdateItem(Item item)
        {
            User user;
            Item updatedItem;

            try
            {
                if (item.NewAskCID != 0)
                {
                    user = GetUserByCID(item.NewAskCID);
                    updatedItem = GetItem(item.Id);
                    if (updatedItem != null && user != null)
                    {
                        if (updatedItem.FirstPrice == 0 || item.NewPrice < updatedItem.FirstPrice)
                        {
                            updatedItem.FirstPrice = item.NewPrice;
                            updatedItem.FirstPriceDisplay = item.NewPrice.ToString();
                            //updatedItem.FirstAskUser = user;
                        }
                        else if (updatedItem.SecondPrice == 0 || item.NewPrice < updatedItem.SecondPrice)
                        {
                            updatedItem.SecondPrice = item.NewPrice;
                            updatedItem.FirstPriceDisplay = item.NewPrice.ToString();
                            //updatedItem.SecondAskUser = user;
                        }
                        else if (updatedItem.ThirdPrice == 0 || item.NewPrice < updatedItem.ThirdPrice)
                        {
                            updatedItem.ThirdPrice = item.NewPrice;
                            updatedItem.FirstPriceDisplay = item.NewPrice.ToString();
                            //updatedItem.ThirdAskUser = user;
                        }

                        if (CIDToUserDataDic.ContainsKey(user.CID))
                        {
                            if (CIDToUserDataDic[user.CID].AskIdList == null)
                            {
                                CIDToUserDataDic[user.CID].AskIdList = new List<int>();
                            }

                            CIDToUserDataDic[user.CID].AskIdList.Add(updatedItem.Id);
                        }
                    }
                }
                else
                {
                    user = GetUserByCID(item.BidCID);
                    updatedItem = GetItem(item.Id);
                    if (updatedItem != null && user != null)
                    {
                        updatedItem.Amount = item.Amount;
                        updatedItem.DueDate = item.DueDate;
                    }
                }
            }
            catch (Exception exception)
            {
                var ex = exception.ToString();
            }
        }

        public Item GetItem(int id)
        {
            Item result = null;

            try
            {
                if (ItemIdToItemDic.ContainsKey(id))
                {
                    result = ItemIdToItemDic[id];
                }
            }
            catch (Exception)
            {
            }

            return result;
        }

        private int CreateItemId()
        {
            int retVal = 0;

            foreach (var item in ItemIdToItemDic)
            {
                if (item.Key > retVal)
                {
                    retVal = item.Key;
                }
            }

            retVal++;

            return retVal;
        }

        public void Init()
        {
            InitItems();

            InitUsers();
        }
        
        private void InitItems()
        {
            Item item1 = new Item()
            {
                Amount = 10,
                Category = "מוצרי חשמל",
                CategoryId = 2,
                DueDate = DateTime.Now.AddDays(1),
                FirstPrice = 100,
                Id = 1,
                Product = "LCD",
                ProductId = 20201,
                SubCategory = "טלויזיה",
                SubCategoryId = 202,
                FirstPriceDisplay = "100"
            };

            ItemIdToItemDic.Add(item1.Id, item1);

            Item item2 = new Item()
            {
                Amount = 12,
                Category = "כלי עבודה",
                CategoryId = 1,
                DueDate = DateTime.Now.AddDays(2),
                FirstPrice = 10,
                Id = 2,
                Product = "מטאטא",
                ProductId = 10103,
                SubCategory = "חומרי עבודה",
                SubCategoryId = 101,
                FirstPriceDisplay = "10"
            };

            ItemIdToItemDic.Add(item2.Id, item2);

            Item item3 = new Item()
            {
                Amount = 10,
                Category = "מוצרי חשמל",
                CategoryId = 2,
                DueDate = DateTime.Now.AddDays(1),
                FirstPrice = 100,
                Id = 3,
                Product = "מאוורר תקרה",
                ProductId = 20001,
                SubCategory = "מאוורר",
                SubCategoryId = 200,
                FirstPriceDisplay = "100"
            };

            ItemIdToItemDic.Add(item3.Id, item3);

            Item item4 = new Item()
            {
                Amount = 10,
                Category = "ריהוט",
                CategoryId = 3,
                DueDate = DateTime.Now.AddDays(1),
                FirstPrice = 100,
                Id = 4,
                Product = "ארון קיר",
                ProductId = 30001,
                SubCategory = "ארונות",
                SubCategoryId = 300,
                FirstPriceDisplay = "100"
            };

            ItemIdToItemDic.Add(item4.Id, item4);

            Item item5 = new Item()
            {
                Amount = 10,
                Category = "מוצרי חשמל",
                CategoryId = 2,
                DueDate = DateTime.Now.AddDays(1),
                FirstPrice = 100,
                Id = 5,
                Product = "LED",
                ProductId = 20202,
                SubCategory = "טלויזיות",
                SubCategoryId = 202,
                FirstPriceDisplay = "100"
            };

            ItemIdToItemDic.Add(item5.Id, item5);

            Item item6 = new Item()
            {
                Amount = 12,
                Category = "ריהוט",
                CategoryId = 3,
                DueDate = DateTime.Now.AddDays(-1),
                FirstPrice = 10,
                Id = 6,
                Product = "כסא בר",
                ProductId = 30102,
                SubCategory = "כסאות",
                SubCategoryId = 301,
                FirstPriceDisplay = "10"
            };

            ItemIdToItemDic.Add(item6.Id, item6);

            Item item7 = new Item()
            {
                Amount = 10,
                Category = "מוצרי חשמל",
                CategoryId = 2,
                DueDate = DateTime.Now.AddMinutes(-1),
                FirstPrice = 1000,
                Id = 7,
                Product = "מזגן עילי",
                ProductId = 20101,
                SubCategory = "מזגן",
                SubCategoryId = 201,
                FirstPriceDisplay = "1000"
            };

            ItemIdToItemDic.Add(item7.Id, item7);

            //Item item8 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 202
            //};

            //Items.Add(item8);

            //Item item9 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 111,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 202
            //};

            //Items.Add(item9);

            //Item item10 = new Item()
            //{
            //    Amount = 12,
            //    Category = "ציוד משרדי",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(2),
            //    FirstPrice = 10,
            //    Id = 2,
            //    Product = "דפים למדפסת",
            //    ProductId = 2,
            //    SubCategory = "דפים",
            //    SubCategoryId = 1
            //};

            //Items.Add(item10);

            //Item item11 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 202
            //};

            //Items.Add(item11);

            //Item item12 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 202
            //};

            //Items.Add(item12);

            //Item item13 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 202
            //};

            //Items.Add(item13);

            //Item item14 = new Item()
            //{
            //    Amount = 12,
            //    Category = "ציוד משרדי",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(2),
            //    FirstPrice = 10,
            //    Id = 2,
            //    Product = "דפים למדפסת",
            //    ProductId = 2,
            //    SubCategory = "דפים",
            //    SubCategoryId = 1
            //};

            //Items.Add(item14);

            //Item item15 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 202
            //};

            //Items.Add(item15);

            //Item item16 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item16);

            //Item item17 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item17);

            //Item item18 = new Item()
            //{
            //    Amount = 12,
            //    Category = "ציוד משרדי",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(2),
            //    FirstPrice = 10,
            //    Id = 2,
            //    Product = "דפים למדפסת",
            //    ProductId = 2,
            //    SubCategory = "דפים",
            //    SubCategoryId = 1
            //};

            //Items.Add(item18);

            //Item item19 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item19);

            //Item item20 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item20);

            //Item item21 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item21);

            //Item item22 = new Item()
            //{
            //    Amount = 12,
            //    Category = "ציוד משרדי",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(2),
            //    FirstPrice = 10,
            //    Id = 2,
            //    Product = "דפים למדפסת",
            //    ProductId = 2,
            //    SubCategory = "דפים",
            //    SubCategoryId = 1
            //};

            //Items.Add(item22);

            //Item item23 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item23);

            //Item item24 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item24);

            //Item item25 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item25);

            //Item item26 = new Item()
            //{
            //    Amount = 12,
            //    Category = "ציוד משרדי",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(2),
            //    FirstPrice = 10,
            //    Id = 2,
            //    Product = "דפים למדפסת",
            //    ProductId = 2,
            //    SubCategory = "דפים",
            //    SubCategoryId = 1
            //};

            //Items.Add(item26);

            //Item item27 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item27);

            //Item item28 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item28);

            //Item item29 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item29);

            //Item item30 = new Item()
            //{
            //    Amount = 12,
            //    Category = "ציוד משרדי",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(2),
            //    FirstPrice = 10,
            //    Id = 2,
            //    Product = "דפים למדפסת",
            //    ProductId = 2,
            //    SubCategory = "דפים",
            //    SubCategoryId = 1
            //};

            //Items.Add(item30);

            //Item item31 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item31);

            //Item item32 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item32);

            //Item item33 = new Item()
            //{
            //    Amount = 10,
            //    Category = "מוצרי חשמל",
            //    CategoryId = 1,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "טלויזיה",
            //    ProductId = 1,
            //    SubCategory = "טלויזיות",
            //    SubCategoryId = 1
            //};

            //Items.Add(item33);

            //Items = new List<Item>();
            //Item item70 = new Item()
            //{
            //    Amount = 10,
            //    Category = "category 1",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "message 1",
            //    ProductId = 1,
            //    SubCategory = "sub cat 1",
            //    SubCategoryId = 201,
            //    Text = "like a new product"
            //};

            //Items.Add(item70);

            //Item item71 = new Item()
            //{
            //    Amount = 10,
            //    Category = "category 1",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "message 1",
            //    ProductId = 1,
            //    SubCategory = "sub cat 1",
            //    SubCategoryId = 201,
            //    Text = "Only in black"
            //};

            //Items.Add(item71);

            //Item item72 = new Item()
            //{
            //    Amount = 10,
            //    Category = "category 1",
            //    CategoryId = 2,
            //    DueDate = DateTime.Now.AddDays(1),
            //    FirstPrice = 100,
            //    Id = 1,
            //    Product = "message 1",
            //    ProductId = 1,
            //    SubCategory = "sub cat 1",
            //    SubCategoryId = 201,
            //    Text = "Great performance"
            //};

            //Items.Add(item72);

        }

        /// <summary>
        /// Users
        /// </summary>

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            try
            {
                UserData userDataRes = CIDToUserDataDic.FirstOrDefault(userData => 
                    userData.Value.User.Username.ToLower().Equals(username.ToLower()) 
                    && userData.Value.User.Password.Equals(password)).Value;
                if (userDataRes != null)
                {
                    return userDataRes.User;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                UserData userDataRes = CIDToUserDataDic.FirstOrDefault(userData =>
                userData.Value.User.Username.ToLower().Equals(username.ToLower())).Value;
                if (userDataRes != null)
                {
                    return userDataRes.User;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                UserData userDataRes = CIDToUserDataDic.FirstOrDefault(userData =>
                userData.Value.User.Email.ToLower().Equals(email.ToLower())).Value;
                if (userDataRes != null)
                {
                    return userDataRes.User;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserByCID(int CID)
        {
            User retVal = null;
            if (CIDToUserDataDic.ContainsKey(CID))
            {
                UserData userDataRes = CIDToUserDataDic[CID];
                if (userDataRes != null)
                {
                    retVal = userDataRes.User;
                }
            }
            
            return retVal;
        }

        public List<Item> GetUserItemListByCID(int CID, bool isBid)
        {
            List<Item> retVal = null;

            if (CIDToUserDataDic.ContainsKey(CID))
            {
                UserData userData = CIDToUserDataDic[CID];
                if (userData != null)
                {
                    List<int> itemIdList = isBid ? userData.BidIdList : userData.AskIdList;

                    if (itemIdList != null && itemIdList.Count > 0)
                    {
                        retVal = itemIdList.Select(id => ItemIdToItemDic[id]).ToList();
                    }
                }
            }

            return retVal;
        }

        private void InitUsers()
        {
            // Bid
            User user1 = new User()
            {
                Username = "dan",
                Password = "1",
                Email = "dandan53@gmail.com",
                CID = 1,
                IsEmailUpdates = true
            };

            CIDToUserDataDic.Add(user1.CID, new UserData() {User = user1, BidIdList = new List<int>()});
            
            // Bid
            User user2 = new User()
            {
                Username = "carmi",
                Password = "2",
                Email = "carmilaks@gmail.com",
                CID = 2,
                IsEmailUpdates = true
            };

            CIDToUserDataDic.Add(user2.CID, new UserData() { User = user2, BidIdList = new List<int>() });

            // Ask
            User user3 = new User()
            {
                Username = "chen",
                Password = "3",
                Email = "chenvardi9@gmail.com",
                CID = 3,
                IsEmailUpdates = true
            };

            CIDToUserDataDic.Add(user3.CID, new UserData() { User = user3, AskIdList = new List<int>() });


            var i = 0;
            var items = DAL.instance.GetItems();
            foreach (var itemPair in items)
            {
                var item = itemPair.Value;
                i++;
                if (i % 2 == 0)
                {
                    item.BidCID = user2.CID;
                    CIDToUserDataDic[user2.CID].BidIdList.Add(item.Id);
                }
                else
                {
                    item.BidCID = user1.CID;
                    CIDToUserDataDic[user1.CID].BidIdList.Add(item.Id);
                }

                item.FirstAskCID = user3.CID;
                CIDToUserDataDic[user3.CID].AskIdList.Add(item.Id);
            }
        }

        public bool SaveUserSettings(SettingsRequest request)
        {
            if (request != null)
            {
                var user = DAL.Instance.GetUserByCID(request.CID);
                if (user != null && user.CID > 0)
                {
                    user.IsEmailUpdates = request.IsEmailUpdates;

                    if (CIDToUserDataDic.ContainsKey(user.CID))
                    {
                        if (CIDToUserDataDic[user.CID].SubscribedProductIdList == null)
                        {
                            CIDToUserDataDic[user.CID].SubscribedProductIdList = new List<int>();
                        }

                        foreach (KeyValuePair<int, int> entry in request.SubscribedProductIdDic)
                        {
                            if (!CIDToUserDataDic[user.CID].SubscribedProductIdList.Contains(entry.Key))
                            {
                                CIDToUserDataDic[user.CID].SubscribedProductIdList.Add(entry.Key);
                            }

                            if (!ProductIdToSubscriberCIDList.ContainsKey(entry.Key))
                            {
                                ProductIdToSubscriberCIDList.Add(entry.Key, new List<int>());
                            }

                            if (!ProductIdToSubscriberCIDList[entry.Key].Contains(user.CID))
                            {
                                ProductIdToSubscriberCIDList[entry.Key].Add(user.CID);
                            }
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        // Register //

        public User AddUser(RegisterRequest registerRequest)
        {
            User existedUser = GetUserByUsername(registerRequest.Username);
            if (existedUser == null)
            {
                var user = new User()
                {
                    CID = CreateUserCId(),
                    Username = registerRequest.Username,
                    Password = registerRequest.Password,
                    Email = registerRequest.Email.ToLower()
                };

                CIDToUserDataDic.Add(user.CID, new UserData() { User = user });

                return user;
            }
            
            return null;
        }

        private int CreateUserCId()
        {
            int retVal = 0;

            foreach (var userData in CIDToUserDataDic)
            {
                if (userData.Value.User.CID > retVal)
                {
                    retVal = userData.Value.User.CID;
                }
            }

            retVal++;

            return retVal;
        }
        
    }
}