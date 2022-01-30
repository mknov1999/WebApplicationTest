using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class ProductsController : Controller
    {
        
        public ActionResult Index()
        {
            IEnumerable<Product> products = null;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44306/api/ApiProducts");

            var productapi = httpClient.GetAsync("ApiProducts");
            productapi.Wait();

            var result = productapi.Result;
            if (result.IsSuccessStatusCode)
            {
                var displayData = result.Content.ReadAsAsync<IList<Product>>();
                displayData.Wait();

                products = displayData.Result;
            }
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44306/api/ApiProducts");

            var productRecord = httpClient.PostAsJsonAsync<Product>("ApiProducts", product);
            productRecord.Wait();

            var result = productRecord.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult Details(Guid id)
        {
            Product product = null;

            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44306/api/");

            var productApi = http.GetAsync("ApiProducts?id=" + id.ToString());
            productApi.Wait();

            var result = productApi.Result;
            if (result.IsSuccessStatusCode)
            {
                var displayData = result.Content.ReadAsAsync<Product>();
                displayData.Wait();
                product = displayData.Result;
            }
            return View(product);
        }

        public ActionResult Edit(Guid id)
        {
            Product product = null;

            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44306/api/");

            var productApi = http.GetAsync("ApiProducts?id=" + id.ToString());
            productApi.Wait();

            var result = productApi.Result;
            if (result.IsSuccessStatusCode)
            {
                var displayData = result.Content.ReadAsAsync<Product>();
                displayData.Wait();
                product = displayData.Result;
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44306/api/ApiProducts");

            var productRecord = http.PutAsJsonAsync<Product>("ApiProducts", product);
            productRecord.Wait();

            var result = productRecord.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Product Record Not Updates  ... !";
            }
            return View(product);
        }

        public ActionResult Delete(Guid id)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44306/api/ApiProducts");

            var delRecord = http.DeleteAsync("ApiProducts/" + id.ToString());
            delRecord.Wait();

            var result = delRecord.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}