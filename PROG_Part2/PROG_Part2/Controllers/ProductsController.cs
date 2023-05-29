using PROG_Part2.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PROG_Part2.Controllers
{
    /// <summary>
    /// controller that handles product actions in product views
    /// </summary>
    public class ProductsController : Controller 
    {
        private FarmCentralDBEntities1 db = new FarmCentralDBEntities1();

        // GET: Products with logged in farmerid
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Where(p => p.FarmerId == SessionInfo.UserId);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,ProductName,ProductType,DateSupplied")] Product product)
        {
            if (ModelState.IsValid)
            {   try
                {
                    product.FarmerId = SessionInfo.UserId;
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                } catch (Exception e)
                {
                    ViewBag.Message = "Error: " + e.Message;
                    return View(product);
                }
            }
            ViewBag.Message = "Product not added successfully";
            return View(product);
        }

        // GET: Products/Edit/
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ProductName,ProductType,DateSupplied")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    product.FarmerId = SessionInfo.UserId;
                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error: Product Not Edited >> " + e.Message;
                    return View(product);
                }
            }
            return View(product);
        }

        // GET: Products/Delete/
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try { 
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }  catch (Exception e)
            {
                ViewBag.Message = "Error: Product Not Deleted >> " + e.Message;
                return View();
            }
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
