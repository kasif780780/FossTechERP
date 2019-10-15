using FossERp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FossERp.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.LeadCount = db.Leads.Count();
            //ViewBag.order = db.SalesOrders.Count();
            ViewBag.Users = db.Users.Count();
//            ViewBag.product = db.Products.Count();
//            ViewBag.product = db.Products.Count();
            ViewBag.product = db.Products.Count();
            ViewBag.pyroll = db.Payrolls.Sum(e => e.Amount);
            ViewBag.emp = db.Employees.Count();
            ViewBag.expense = db.Expenses.Sum(d => d.Amount);
            ViewBag.orders = db.Orders.Count();
            ViewBag.customer = db.Customers.Count();
            ViewBag.purchaseOrders = db.PurchaseOrders.Sum(e => e.Products.Sum(p => p.Product.UnitPrice));
            return View();
    }

    public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}