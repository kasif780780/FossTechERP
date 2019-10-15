using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FossERp.Models;

namespace FossERp.Controllers
{

    [Authorize(Roles = "Admin,SuperAdmin,Manager")]
    public class PayrollsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payrolls
        public ActionResult Index()
        {
            var payrolls = db.Payrolls.Include(p => p.Employee);
            return View(payrolls.OrderByDescending(e => e.PaymentDate).ToList());
        }

        // GET: Payrolls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // GET: Payrolls/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeID", "FullName");
            return View();
        }

        // POST: Payrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Payroll payroll,HttpPostedFileBase file)
        {
          
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {

                    string name = Path.GetFileName(file.FileName);
                    string path = "~/Images/" + name;

                    file.SaveAs(Server.MapPath(path));
                    db.Payrolls.Add(new Payroll
                    {
                        PayrollID = payroll.PayrollID,
                        Amount = payroll.Amount,
                        PaymentType = payroll.PaymentType,
                        PaymentDate = payroll.PaymentDate,
                        EmployeeId = payroll.EmployeeId,
                        ImagePath = path
                    });
                    db.SaveChanges();
                }
                else
                {
                    db.Payrolls.Add(new Payroll
                    {
                        PayrollID = payroll.PayrollID,
                        Amount = payroll.Amount,
                        PaymentType = payroll.PaymentType,
                        PaymentDate = payroll.PaymentDate,
                        EmployeeId = payroll.EmployeeId
                        
                    });
                    db.SaveChanges();
                }

               
             
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeID", "FullName", payroll.EmployeeId);
            return View(payroll);
        }

        // GET: Payrolls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeID", "FullName", payroll.EmployeeId);
            return View(payroll);
        }

        // POST: Payrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayrollID,Amount,PaymentDate,PaymentType,ImagePath,EmployeeId")] Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeID", "FullName", payroll.EmployeeId);
            return View(payroll);
        }

        // GET: Payrolls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // POST: Payrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payroll payroll = db.Payrolls.Find(id);
            db.Payrolls.Remove(payroll);
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
