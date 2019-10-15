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
    public class ExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Expenses
        public ActionResult Index()
        {
            return View(db.Expenses.OrderByDescending(e=>e.DateOfSpend).ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expense expense,HttpPostedFileBase file)
        {
           
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string name = Path.GetFileName(file.FileName);
                    string path = "~/Images/" + name;
                    file.SaveAs(Server.MapPath(path));

                    db.Expenses.Add(new Expense
                    {
                        Id = expense.Id,
                        Currency = expense.Currency,
                        Amount = expense.Amount,
                        Purpose = expense.Purpose,
                        DateOfSpend = expense.DateOfSpend,
                        Marchant = expense.Marchant,
                        Category = expense.Category,
                        Description = expense.Description,
                        ImagePath = path
                    });
                    db.SaveChanges();
                }
                else
                {
                    db.Expenses.Add(new Expense
                    {
                        Id = expense.Id,
                        Currency = expense.Currency,
                        Amount = expense.Amount,
                        Purpose = expense.Purpose,
                        DateOfSpend = expense.DateOfSpend,
                        Marchant = expense.Marchant,
                        Category = expense.Category,
                        Description = expense.Description
                        
                    });
                    db.SaveChanges();
                }
              
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Currency,Amount,Purpose,DateOfSpend,Marchant,Category,Description,ImagePath")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
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
