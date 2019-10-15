using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FossERp.Models;
using FossERp.ViewModels;
using PagedList;

namespace FossERp.Controllers
{

    [Authorize(Roles = "Admin,SuperAdmin,Manager")]
    public class SalesOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SalesOrders
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var salesOrders = db.salesOrders.Include(s => s.Lead);
           
            if (!String.IsNullOrEmpty(searchString))
            {
                salesOrders = salesOrders.Where(s => s.CompanyName.Contains(searchString)
                                       || s.SalesPerson.Contains(searchString) || s.LeadID.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    salesOrders = salesOrders.OrderByDescending(s => s.CompanyName);
                    break;
                case "Date":
                    salesOrders = salesOrders.OrderBy(s => s.SaleConfirmationDate);
                    break;
                case "date_desc":
                    salesOrders = salesOrders.OrderByDescending(s => s.SalesPerson);
                    break;
                default:
                    salesOrders = salesOrders.OrderBy(s => s.CompanyName);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(salesOrders.OrderByDescending(x=>x.SaleConfirmationDate).ToPagedList(pageNumber, pageSize));
         
        }

        // GET: SalesOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.salesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }

        // GET: SalesOrders/Create
        public ActionResult Create()
        {
            ViewBag.LeadID = new SelectList(db.Leads, "LeadID", "LeadID");
            return View();
        }

        // POST: SalesOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesOrderID,SaleConfirmationDate,CompanyName,SalesPerson,TotalPrice,MIBStatus,MIBHold,activation,LeadID")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                db.salesOrders.Add(salesOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeadID = new SelectList(db.Leads, "LeadID", "LeadID", salesOrder.LeadID);
            return View(salesOrder);
        }

        // GET: SalesOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.salesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeadID = new SelectList(db.Leads, "LeadID", "LeadID", salesOrder.LeadID);
            return View(salesOrder);
        }

        // POST: SalesOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderID,SaleConfirmationDate,CompanyName,SalesPerson,TotalPrice,MIBStatus,MIBHold,activation,LeadID")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeadID = new SelectList(db.Leads, "LeadID", "LeadID", salesOrder.LeadID);
            return View(salesOrder);
        }

        // GET: SalesOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.salesOrders.Where(x => x.SalesOrderID == id).FirstOrDefault();
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
          


           
           
           
            return View(salesOrder);
        }

        // POST: SalesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrder salesOrder = db.salesOrders.Find(id);
            db.salesOrders.Remove(salesOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
