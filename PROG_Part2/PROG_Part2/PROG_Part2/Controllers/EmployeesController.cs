using PROG_Part2.Controllers.IController;
using PROG_Part2.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PROG_Part2.Controllers
{
    public class EmployeesController : Controller, IEmployees
    {
        /// <summary>
        /// Controller that handles all the employee related actions for the employee model
        /// </summary>

        private FarmCentralDBEntities1 db = new FarmCentralDBEntities1();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            db.Database.Log = s => Debug.WriteLine(s);
            return View(await db.Farmers.ToListAsync());
        }

        // GET: Employees/Details/
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "No Employee Selected";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Database.Log = s => Debug.WriteLine(s);
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                ViewBag.Message = "Employee Not Found";
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Farmers/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = SessionInfo.UserId;
            return View();
        }

        // POST: Farmers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    farmer.EmployeeId = SessionInfo.UserId;
                    db.Farmers.Add(farmer);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                } catch (Exception e)
                {
                    ViewBag.Message = "Error: Farmer Not Created >> " + e.Message;
                    return View(farmer);
                }
            }
            ViewBag.Message = "Farmer Not Created";
            ViewBag.EmployeeId = SessionInfo.UserId;
            return View(farmer);
        }

        // GET: Employees/Edit/
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeId,EmpName,EmpSurname,EmpPassword")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(employee).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error: Employee not edited >> " + e.Message;
                    return View(employee);
                }
            }
            ViewBag.Message = "Employee Not Edited";
            return View(employee);
        }

        // GET: Employees/Delete/
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try { 
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        } catch (Exception e)
            {
                ViewBag.Message = "Error: Employee not deleted >> " + e.Message;
                return View();
            }
        }

        //check if employee exists in database
        public Employee CheckLogin(int username, string password)
        {
            using (var db = new FarmCentralDBEntities1())
            {
                db.Database.Log = s => Debug.WriteLine(s);
                var result = db.Employees.FirstOrDefault(p => p.EmployeeId == username && p.EmpPassword == password);
                return result;
            }
        }

        //renders partial view for the farmer list
        public PartialViewResult Farmers()
        {
            return PartialView("Farmers");
        }

        //action method to display and filter farmer products
        public ActionResult FarmerProducts(string farmerId, string searchString = null, DateTime? startdate = null, DateTime? enddate = null)
        {
            try
            {
                ViewBag.FarmerList = db.Farmers.Distinct().Select(x => new SelectListItem
                {
                    Text = x.FarmerId.ToString(),
                    Value = x.FarmerId.ToString(),
                }).ToList();
                ViewBag.FarmerId = farmerId;
                var products = db.Products.ToList();
                db.Database.Log = s => Debug.WriteLine(s);
                if (farmerId != null)
                {
                    var fProducts = from p in db.Products.Where(x => x.FarmerId.ToString().Equals(farmerId)) select p;

                    if (startdate != null && enddate != null)
                    {
                        db.Database.Log = s => Debug.WriteLine(s);
                        fProducts = (IQueryable<Product>)fProducts.Where(t => (!startdate.HasValue || t.DateSupplied >= startdate) && (!enddate.HasValue || t.DateSupplied <= enddate));
                    }
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        fProducts = fProducts.Where(p => p.ProductType.Contains(searchString));
                    }
                    return View(fProducts.ToList());
                }
                else
                {
                    return View(products);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
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

        // GET: Farmers/Details/
        public async Task<ActionResult> FarmerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = await db.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        // GET: Farmers/Edit/
        public async Task<ActionResult> FarmerEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = await db.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        // POST: Farmers/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FarmerEdit([Bind(Include = "FarmerId,FarmerFName,FarmerSName,FarmerCellPhoneNum,FarmerPassword")] Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    farmer.EmployeeId = SessionInfo.UserId;
                    db.Entry(farmer).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error: Farmer Not Edited >> " + e.Message;
                    return View(farmer);
                }
            }
            ViewBag.Message = "Farmer Not Edited";
            return View(farmer);
        }

        // GET: Farmers/Delete/
        public async Task<ActionResult> FarmerDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = await db.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        // POST: Farmers/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FarmerDeleteConfirmed(int id)
        {
            try { 
            Farmer farmer = await db.Farmers.FindAsync(id);
            db.Farmers.Remove(farmer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        } catch (Exception e)
            {
                ViewBag.Message = "Error: Farmer not deleted >> " + e.Message;
                return View();
            }
        }
    }
}
