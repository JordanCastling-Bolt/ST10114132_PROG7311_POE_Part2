using PROG_Part2.Controllers.IController;
using System;
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
            var result = _employees.CheckLogin(Convert.ToInt32(form["id"]), form["password"]);
            var result1 = _farmers.CheckLogin(Convert.ToInt32(form["id"]), form["password"]);          

            if (result != null)
            {
                SessionInfo.UserId = result.EmployeeId;
                return RedirectToAction("Index", "Employees");
            }
            else if (result1 != null)
            {
                SessionInfo.UserId = result1.FarmerId;
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewBag.Message = "Invalid login details";
                return View();
            }
        }
    }
}