using FossERp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rotativa.MVC;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.IO;
using Rotativa.Core;
using Newtonsoft.Json;

namespace FossERp.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private object path;

        // GET: Orders
        public ActionResult Index(string currentFilter, string searchString, int? page)

        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            List<Vendor> vendors = db.Vendors.ToList();

            IList<PurchaseOrder> orders = db.PurchaseOrders.ToList();
            var doctors = from s in orders select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.Vendor.Name.Contains(searchString) || s.Vendor.Name.Contains(searchString));
            }

            IList<Product> products = db.Products.ToList();
            ViewData["customers"] = vendors;
            ViewData["products"] = products;
            // return View(orders);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(doctors.OrderByDescending(o => o.Date).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<ActionResult> GetOrderInfo(int id)
        {
            PurchaseOrder order = await db.PurchaseOrders.FindAsync(id);
            if (order != null)
            {
                var data = JsonConvert.SerializeObject(order,
                 Formatting.Indented,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 });


                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        // Get: orders/Edit/1
        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            PurchaseOrder order = await db.PurchaseOrders.FindAsync(id);
            if (order != null)
            {
                return View(order);
            }

            return HttpNotFound();
        }
        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            {
            }
            PurchaseOrder doctor = db.PurchaseOrders.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Invoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            PurchaseOrder order = db.PurchaseOrders.Where(x => x.PurchaseOrderId == id).FirstOrDefault();
           
            return View(order);
        }
        public ActionResult PrintInvoice(int? id)
        {
            if (id == null)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            PurchaseOrder order = db.PurchaseOrders.Where(x => x.PurchaseOrderId == id).FirstOrDefault();
            return new Rotativa.ViewAsPdf("PrintInvoice", order);
        }

        public async Task<ActionResult> SaveOrder(PurchaseOrderViewModel order)
        {
            var newOrder = new PurchaseOrder
            {
               
                VendorID = order.VendorId,
                Status = "Pending",
                Date = DateTime.Now
            };
            db.PurchaseOrders.Add(newOrder);
            await db.SaveChangesAsync();

            foreach (var p in order.Products)
            {
                PurchaseOrderProduct orderProduct = new PurchaseOrderProduct
                {
                    PurchaseOrderId = newOrder.PurchaseOrderId,
                    ProductId = p.ProductId,
                    Qty = p.Qty
                };
                db.PurchaseOrderProducts.Add(orderProduct);
                await db.SaveChangesAsync();
            }
            return Json(new { Id = newOrder.PurchaseOrderId, Date = newOrder.Date });
        }

        // GET: MyLead/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: MyLead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder order = db.PurchaseOrders.Find(id);
            db.PurchaseOrders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Invoice(int id)
        {
            PurchaseOrder order = await db.PurchaseOrders.FindAsync(id);
            return View("PrintInvoice", order);
        }

        //[HttpGet, ActionName("SendInvoice")]
        //public async Task<ActionResult> SendInvoice(int id)
        //{
        //    PurchaseOrder order = await db.PurchaseOrders.FindAsync(id);
        //    if (order != null)
        //    {
        //        Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
        //        foreach (var key in Request.Cookies.AllKeys)
        //        {
        //            cookieCollection.Add(key, Request.Cookies.Get(key).Value);
        //        }

        //        ActionAsPdf pdf = new ActionAsPdf("Invoice", new { id })
        //        {
        //            FileName = $"Order#{order.PurchaseOrderId}-{DateTime.Now.ToFileTime()}.pdf",
        //            RotativaOptions = new DriverOptions
        //            {
        //                Cookies = cookieCollection,
        //            }
        //        };

        //        var message = new MimeMessage();
        //        message.From.Add(new MailboxAddress("Kishor", "kishorgujar95@gmail.com"));
        //        message.To.Add(new MailboxAddress($"{order.Vendor.Name} {order.Vendor.Name}", $"{order.Vendor.Email}"));
        //        message.Subject = $"Order#{order.Id} Invoice";

        //        var builder = new BodyBuilder();
        //        // Set the plain-text version of the message text

        //        builder.TextBody = $"Hey {order.Customer.FirstName} {order.Customer.LastName}, Here is your invoice for Order#{order.Id}";

        //        // We may also want to attach a calendar event for Monica's party...
        //        builder.Attachments.Add(pdf.FileName, pdf.BuildPdf(ControllerContext));

        //        // Now we just need to set the message body and we're done
        //        message.Body = builder.ToMessageBody();



        //        using (var client = new SmtpClient())
        //        {
        //            client.Connect("smtp.gmail.com", 587);

        //            // Note: since we don't have an OAuth2 token, disable
        //            // the XOAUTH2 authentication mechanism.
        //            client.AuthenticationMechanisms.Remove("XOAUTH2");

        //            // Note: only needed if the SMTP server requires authentication
        //            client.Authenticate("kishorgujar95@gmail.com", "kxbcyuqhwtbhzjml");

        //            client.Send(message);
        //            client.Disconnect(true);
        //            return Json("Email Sent", JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    return HttpNotFound();
        //}

        [HttpPost]
        public async Task<ActionResult> UpdateOrder(PurchaseOrderViewModel order)
        {
            var updateOrder = await db.PurchaseOrders.FindAsync(order.ID);
            if (updateOrder == null)
            {
                return HttpNotFound();
            }

            var Products = await db.PurchaseOrderProducts.Where(p => p.PurchaseOrderId == order.ID).ToListAsync();
            db.PurchaseOrderProducts.RemoveRange(Products);

            foreach (var p in order.Products)
            {
                PurchaseOrderProduct orderProduct = new PurchaseOrderProduct
                {
                    PurchaseOrderId = int.Parse(order.ID.ToString()),
                    ProductId = p.ProductId,
                    Qty = p.Qty
                };
                db.PurchaseOrderProducts.Add(orderProduct);
                await db.SaveChangesAsync();
            }
            return Json(new { updateOrder.PurchaseOrderId, updateOrder.Date });
        }
    }

    public class PurchaseOrderViewModel
    {
        public int? ID { get; set; }
        public int VendorId { get; set; }
        public ProductViewModel[] Products { get; set; }
    }

    public class VProductViewModel
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }

    }
}
