﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FossERp.Models;

namespace FossERp.Controllers
{
    public class TestLeadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TestLead
        public ActionResult Index()
        {
            return View(db.Leads.ToList());
        }

        // GET: TestLead/Details/5
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

        // GET: TestLead/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestLead/Create
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

        // GET: TestLead/Edit/5
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

        // POST: TestLead/Edit/5
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

        // GET: TestLead/Delete/5
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

        // POST: TestLead/Delete/5
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
