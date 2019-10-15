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
using PagedList;

namespace FossERp.Controllers
{
  
    [Authorize(Roles ="Admin,SuperAdmin,Manager")]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
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
            var emplyoee = from l in db.Employees
                        select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                emplyoee = emplyoee.Where(s => s.FullName.Contains(searchString)
                                       || s.IdentificationNo.Contains(searchString) || s.Location.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    emplyoee = emplyoee.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    emplyoee = emplyoee.OrderBy(s => s.IdentificationNo);
                    break;
                case "date_desc":
                    emplyoee = emplyoee.OrderByDescending(s => s.Location);
                    break;
                default:
                    emplyoee = emplyoee.OrderBy(s => s.FullName);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(emplyoee.OrderByDescending(e=>e.JoiningDate).ToPagedList(pageNumber, pageSize));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Employee employee,HttpPostedFileBase file)
        {


           



            if (ModelState.IsValid)
            {


                if (file != null && file.ContentLength > 0)
                {
                    string name = Path.GetFileName(file.FileName);
                    string path = "~/Images/" + name;
                    file.SaveAs(Server.MapPath(path));

                    db.Employees.Add(new Employee
                    {
                        EmployeeID = employee.EmployeeID,
                        FullName = employee.FullName,
                        JoiningDate = employee.JoiningDate,
                        JobTitle = employee.JobTitle,
                        Mobile = employee.Mobile,
                        Email = employee.Email,
                        Department = employee.Department,
                        Position = employee.Position,
                        Address = employee.Address,
                        Location = employee.Location,
                        BankAccount = employee.BankAccount,
                        BankIFSC = employee.BankIFSC,
                        DateofBirth = employee.DateofBirth,
                        IdentificationNo = employee.IdentificationNo,
                        CertificateLavel = employee.CertificateLavel,
                        FieldofStudy = employee.FieldofStudy,
                        Document = employee.Document,
                        ImagePath = path




                    });
                    db.SaveChanges();
                }

                else
                {
                    db.Employees.Add(new Employee
                    {
                        EmployeeID = employee.EmployeeID,
                        FullName = employee.FullName,
                        JoiningDate = employee.JoiningDate,
                        JobTitle = employee.JobTitle,
                        Mobile = employee.Mobile,
                        Email = employee.Email,
                        Department = employee.Department,
                        Position = employee.Position,
                        Address = employee.Address,
                        Location = employee.Location,
                        BankAccount = employee.BankAccount,
                        BankIFSC = employee.BankIFSC,
                        DateofBirth = employee.DateofBirth,
                        IdentificationNo = employee.IdentificationNo,
                        CertificateLavel = employee.CertificateLavel,
                        FieldofStudy = employee.FieldofStudy,
                        Document = employee.Document
                       




                    });
                    db.SaveChanges();
                }
              
                return RedirectToAction("Index");


            }
           
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,FullName,JoiningDate,JobTitle,Mobile,Email,Department,Position,Address,Location,BankAccount,BankIFSC,Gender,DateofBirth,IdentificationNo,CertificateLavel,FieldofStudy,Document,ImagePath")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
