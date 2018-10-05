using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeTravel.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;


using System.Data;
using System.Net;

namespace EmployeeTravel.Controllers
{
    public class EmployeeController : Controller
    {
        Entities dbcontext = new Entities();
        Entities1 db = new Entities1();



        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Home()
        {
            return View();
        }


        // GET: Employee
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]

        public ActionResult Login(string txtEmail, string txtpass)
        { 
            List<Employee> emplist = dbcontext.Employees.ToList();
            var logincheck = (from check in emplist
                              where check.EmployeeEmail == txtEmail && check.EmployeePassword == txtpass
                              select check).ToList();
            if (logincheck.Count != 0)
            {
                Response.Redirect("Home");
            }
            else
            {
                return View("Login");
            }
            return View();
        }






        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public ActionResult Register(Employee obj)

        {
            //if (ModelState.IsValid)
            //{

                //if (string.IsNullOrEmpty(obj.EmployeeName.ToString()))
                //{
                //    ModelState.AddModelError("EmployeeName", "TravelStartdate is Required ");
                //}

                //if (string.IsNullOrEmpty(obj.EmployeeEmail.ToString()))
                //{
                //    ModelState.AddModelError("EmployeeEmail", " TravelEnddate is Required ");
                //}
                //if (string.IsNullOrEmpty(obj.EmployeePassword.ToString()))
                //{
                //    ModelState.AddModelError("EmployeePassword", "TravelSource is Required ");
                //}
                //if (string.IsNullOrEmpty(obj.EmployeePhoneNo.ToString()))
                //{
                //    ModelState.AddModelError("EmployeePhoneNo", "TravelDestination is Required ");
                //}

                if (ModelState.IsValid)
                {
                    Entities db = new Entities();
                    db.Employees.Add(obj);
                    db.SaveChanges();
                    Response.Redirect("RegisterSuccess");
                }
            //}
            return View(obj);

        }



        public ActionResult Travel()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Travel(Travel obj)

        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(obj.TravelStartdate.ToString()))
                {
                    ModelState.AddModelError("TravelStartdate", "TravelStartdate is Required ");
                }

                if (string.IsNullOrEmpty(obj.TravelEnddate.ToString()))
                {
                    ModelState.AddModelError("TravelEnddate", " TravelEnddate is Required ");
                }
                if (string.IsNullOrEmpty(obj.TravelSource))
                {
                    ModelState.AddModelError("TravelSource", "TravelSource is Required ");
                }
                if (string.IsNullOrEmpty(obj.TravelDestination))
                {
                    ModelState.AddModelError("TravelDestination", "TravelDestination is Required ");
                }

                if (ModelState.IsValid)
                {
                    Entities1 db = new Entities1();
                    db.Travels.Add(obj);
                    db.SaveChanges();
                    Response.Redirect("TravelSuccess");
                }
            }
            return View(obj);

        }


        public ActionResult FilterSearch(int? id)

        {



            var result = from p in db.Travels

                         where p.TravelID == id

                         select p;

            return View(result.ToList());





        }



        public ActionResult FilterCancelView(int? id)

        {
            var result = from p in db.Travels

                         where p.TravelID == id

                         select p;

            return View(result.ToList());

        }



        public ActionResult ViewOrder()

        {

            return View(db.Travels.ToList());

        }



        [HttpGet]
        public ActionResult Delete(int? id)

        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Travel travel = db.Travels.Find(id);

            if (travel == null)

            {

                return HttpNotFound();

            }

            return View(travel);

        }



        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)

        {

            Travel travel = db.Travels.Find(id);

            db.Travels.Remove(travel);

            db.SaveChanges();

            return RedirectToAction("Home");

        }


        
        
        public ActionResult TravelSuccess(int? id)
        {
            Entities1 hmd = new Entities1();
            int travelid = Convert.ToInt32(hmd.Database.SqlQuery<decimal>("Select IDENT_CURRENT ('Group3.Travel')", new object[0]).FirstOrDefault());
            ViewBag.TravelID = travelid;
        
            return View();
        }

        public ActionResult RegisterSuccess(int? id)
        {
            Entities hmd = new Entities();
            int Employeeid = Convert.ToInt32(hmd.Database.SqlQuery<decimal>("Select IDENT_CURRENT ('Group3.Employee')", new object[0]).FirstOrDefault());
            ViewBag.EmployeeID = Employeeid;

            return View();
        }











    }
}