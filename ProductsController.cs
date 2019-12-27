using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EClient.Models;

namespace EClient.Controllers
{
    public class ProductsController : Controller
    {
        private string BASE_URL = "http://localhost:53080//ServiceProduct.svc/";

        // GET: Products
        public ActionResult Index()
        {
            var webclient = new WebClient();
            var json = webclient.DownloadString(BASE_URL + "findall");
            var js = new JavaScriptSerializer();
            List<Product> proList = js.Deserialize<List<Product>>(json);

            //ViewBag.listProducts = proList;
            return View(proList);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var webclient = new WebClient();
            var json = webclient.DownloadString(BASE_URL + "find/" + id);
            var js = new JavaScriptSerializer();
            Product pro = js.Deserialize<Product>(json);

            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,category,name,price,description")] Product product)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Product));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, product);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString(BASE_URL + "create", "POST", data);

            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var webclient = new WebClient();
            var json = webclient.DownloadString(BASE_URL + "find/" + id);
            var js = new JavaScriptSerializer();
            Product pro = js.Deserialize<Product>(json);

            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,category,name,price,description")] Product product)
        {
            if (ModelState.IsValid)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Product));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, product);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "edit", "PUT", data);

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var webclient = new WebClient();
            var json = webclient.DownloadString(BASE_URL + "find/" + id);
            var js = new JavaScriptSerializer();
            Product pro = js.Deserialize<Product>(json);

            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var webclient = new WebClient();
            var json = webclient.DownloadString(BASE_URL + "find/" + id);
            var js = new JavaScriptSerializer();
            Product pro = js.Deserialize<Product>(json);


            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Product));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, pro);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString(BASE_URL + "delete", "DELETE", data);

            return RedirectToAction("Index");
        }
    }
}
