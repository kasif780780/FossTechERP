using FossERp.Models;
using FossERp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FossERp.Controllers
{
    public class MySalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: MySales
        public ActionResult Index()
        {
            var dbl = new SalesOrderViewodels();
            var lead = (from a in db.Leads select a).ToList();
            var salesOrder = (from c in db.salesOrders select c).ToList();
           
            var model = new SalesOrderViewodels
            {
                _lead = lead,
                _msalesOrder = salesOrder,
               
            };
            return View(model);
        }

        public ActionResult Details()
        {
            var dbl = new SalesOrderViewodels();
            var lead = (from a in db.Leads select a).ToList();
            var salesOrder = (from c in db.salesOrders select c).ToList();

            var model = new SalesOrderViewodels
            {
                _lead = lead,
                _msalesOrder = salesOrder,

            };
            return View(model);
        }
    }
}