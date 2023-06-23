using PROG_Part2.Controllers.IController;
using PROG_Part2.Models;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace PROG_Part2.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// controller for home views(index and login pages)
        /// </summary>
        private readonly IEmployees _employees = new EmployeesController();
        private readonly IFarmers _farmers = new FarmersController();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your login page";

            return View();
        }

        //check if employee id or farmer id user entered exists in the database
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                using (FarmCentralDBEntities1 context = new FarmCentralDBEntities1())
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    var result = _employees.CheckLogin(Convert.ToInt32(form["id"]), form["password"]);
                    var result1 = _farmers.CheckLogin(Convert.ToInt32(form["id"]), form["password"]);
                    if (result != null)
                    {
                        context.Database.Log = s => Debug.WriteLine(s);
                        SessionInfo.UserId = result.EmployeeId;
                        return RedirectToAction("Index", "Employees");
                    }
                    else if (result1 != null)
                    {
                        context.Database.Log = s => Debug.WriteLine(s);
                        SessionInfo.UserId = result1.FarmerId;
                        return RedirectToAction("Index", "Products");
                    }
                    sw.Stop();
                    Debug.WriteLine("Queries took" + sw.ElapsedMilliseconds + " ms");
                }
            }
            catch (Exception)
            {
                ViewBag.Message = "Invalid login details";
                return View(form);
            }
            return View(form);
        }
        public ActionResult Register()
        {
            return View();
        }

        // POST: Employee/Register
        [HttpPost]
        public ActionResult Register(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Save the employee to the database or perform other operations
                // Example: using Entity Framework to save the employee
                using (var dbContext = new FarmCentralDBEntities1())
                {
                    dbContext.Employees.Add(employee);
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Index", "Home"); // Redirect to the homepage or any other desired page
            }

            return View(employee);
        }
    }
}

    
