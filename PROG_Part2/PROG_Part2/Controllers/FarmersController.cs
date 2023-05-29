using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROG_Part2.Models;
using PROG_Part2.Controllers.IController;

namespace PROG_Part2.Controllers
{
    public class FarmersController : Controller , IFarmers
    {
        /// <summary>
        /// controller that handles the farmer login actions
        /// </summary>
        private FarmCentralDBEntities1 db = new FarmCentralDBEntities1();

        // GET: Farmers
        public async Task<ActionResult> Index()
        {
            var farmers = db.Farmers.Include(f => f.Employee);
            return View(await farmers.ToListAsync());
        }

        //checks if farmer exists in the database
        public Farmer CheckLogin(int username, string password)
        {
            using (var db = new FarmCentralDBEntities1())
            {
                var result = db.Farmers.FirstOrDefault(p => p.FarmerId == username && p.FarmerPassword == password);
                return result;
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
