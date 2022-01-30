using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class ApiProductsController : ApiController
    {
        private TestDbEntities db = new TestDbEntities();

        

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Product product)
        {

            var updateProduct = db.Products.Where(x=> x.ID == product.ID).FirstOrDefault<Product>();
            if (updateProduct != null)
            {
                updateProduct.Name = product.Name;
                updateProduct.Description = product.Description;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        [ResponseType(typeof(Product))]
        public IHttpActionResult Post(Product product)
        {
            product.ID = Guid.NewGuid();
            db.Products.Add(product);
            db.SaveChanges();

            return Ok();
        }

        [ResponseType(typeof(Product))]
        public IHttpActionResult Delete(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }
    }
}