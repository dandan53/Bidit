using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Bidit.Models;

namespace Bidit.Controllers
{
    public class ItemController : ApiController
    {
        //private List<Item> Items;

        // GET api/Items
        public IEnumerable<Item> Get(int categoryId = 0, int subCategoryId = 1000, int productId = 0, int CID = 0)
        {
            List<Item> Items = DAL.Instance.GetItems().Values.ToList();

            IEnumerable<Item> filteredItems = null;

            if (categoryId == 0)
            {
                filteredItems = Items;
            }
            else if (subCategoryId == 1000)
            {
                filteredItems = Items.Where(i => i.CategoryId == categoryId);
            }
            else if (productId == 0)
            {
                filteredItems = Items.Where(i => i.CategoryId == categoryId && i.SubCategoryId == subCategoryId);
            }
            else
            {
                filteredItems = Items.Where(i => i.CategoryId == categoryId && i.SubCategoryId == subCategoryId && i.ProductId == productId);
            }

            return filteredItems;
        }

        //// GET api/Items
        // public IEnumerable<Item> Get(string q = null, string sort = null, bool desc = false,
        //                                                 int? limit = null, int offset = 0)
        // {
        //     return Items;
        // }


        // GET api/Todo/5
        public Item Get(int id)
        {
            Item item = DAL.Instance.GetItem(id);
            return item;

            //Item item1 = new Item()
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

            //return item1;
        }

        // DELETE api/Item/5
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Item());
        }

        // POST api/Item
        public HttpResponseMessage PostTodo(Item item)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.Id }));

            if (item.Id == 0)
            {
                DAL.Instance.AddItem(item);                
            }
            else
            {
                DAL.Instance.UpdateItem(item);
            }

            return response;
        }

        // PUT api/Item/5
        public HttpResponseMessage PutTodo(int id, Item item)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private ItemController()
        {
        }

    }
}