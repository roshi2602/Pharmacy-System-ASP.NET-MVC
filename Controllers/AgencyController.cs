using System;
using System.Linq;
using System.Web.Mvc;
using Practice4.Models;
namespace Practice4.Controllers
{
    [Authorize]
    public class AgencyController : Controller
    {
        PracticeaspDBEntities1 db = new PracticeaspDBEntities1();
      
            //for sorting
            [Authorize(Roles = "Chemist, Pharmacist,Manager")]
            public ActionResult Home2(string sort)
            {
            var x = db.Agencies.Select(o => o); //select is used to select whole data

                //apply switch case
                switch (sort)
                {
                    case "descending":
                        x = db.Agencies.OrderByDescending(i => i.Name);
                        break;

                    default:    //orderby for ascending
                        x = db.Agencies.OrderBy(i => i.Name);
                        break;

                }
                return View(x);
            }


        [HttpGet]
        [Authorize(Roles="Manager")]
        public ActionResult Details(int id)
        {
            var x = db.Agencies.FirstOrDefault(y => y.Id == id);
            return View(x);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            var x = db.Agencies.FirstOrDefault(y => y.Id == id);
            return View(x);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(FormCollection val)
        {
            var ide = Convert.ToInt32(val["Id"]);
            var x = db.Agencies.FirstOrDefault(t => t.Id == ide);
            x.Name = val["Name"];
            x.Date =Convert.ToDateTime(val["Date"]);
            x.Amount =Convert.ToDouble(val["Amount"]);
            x.GrandTotal =Convert.ToDouble(val["GrandTotal"]);
            x.IsPaid = val["IsPaid"];
            db.SaveChanges();
            return RedirectToAction("Home2");
        }
    }
}
