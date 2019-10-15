using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FossERp.Models;
using PagedList;

namespace FossERp.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Manager")]
    public class LeadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Leads
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
            var leads = from l in db.Leads
                           select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                leads = leads.Where(s => s.CompanyName.Contains(searchString)
                                       || s.ContactName.Contains(searchString) || s.LeadID.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    leads = leads.OrderByDescending(s => s.CompanyName);
                    break;
                case "Date":
                    leads = leads.OrderBy(s => s.LeadCreationDate);
                    break;
                case "date_desc":
                    leads = leads.OrderByDescending(s => s.ContactName);
                    break;
                default:
                    leads = leads.OrderBy(s => s.CompanyName);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(leads.OrderByDescending(l=>l.LeadCreationDate).ToPagedList(pageNumber, pageSize));
        }

        // GET: Leads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            return View(lead);
        }

        // GET: Leads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeadID,LeadCreationDate,ContactName,Email,Mobile,CompanyName,CompanyEmail,CompanyMobile,Address,LandMark,City,Pincode,LeadCreatorName,Source,BusinessCategory,OurProduct")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                db.Leads.Add(lead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lead);
        }

        // GET: Leads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            return View(lead);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeadID,LeadCreationDate,ContactName,Email,Mobile,CompanyName,CompanyEmail,CompanyMobile,Address,LandMark,City,Pincode,LeadCreatorName,Source,BusinessCategory,OurProduct")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lead);
        }

        // GET: Leads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            return View(lead);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lead lead = db.Leads.Find(id);
            db.Leads.Remove(lead);
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
