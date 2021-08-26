using System;
using System.Linq;
using System.Web.Mvc;
using Practice4.Models;
using Practice4.ViewModel;
using PagedList;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Collections;
namespace Practice4.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {

        PracticeaspDBEntities1 db = new PracticeaspDBEntities1();
        //used Viewmodel
        //install pagedlist.mvc from nuget for pagination concept
        [Authorize(Roles = "Chemist, Pharmacist,Manager")]
        public ActionResult Home(int page=3) //passed value typed parameter for 3 pages                                            //page number
        {
            int recperpage = 3;  //pagesize
            var viewmodel = new List<StoreView>();
 
            viewmodel = db.Stores.ToList().Select(x => new StoreView
            {
                Id = x.Id,
                MedicineName = x.MedicineName,
                ExpiryData = x.ExpiryData,
                ManufacturedDate = x.ManufacturedDate,
                price = x.price,
                PharmEmail = x.PharmEmail,
                PharmNumber = x.PharmNumber,
                PharmWebsite = x.PharmWebsite,
                MedicinePhtot = x.MedicinePhtot

            }).ToList();
            IPagedList<StoreView> yy = viewmodel.ToPagedList(recperpage, page);
            //ToPagedList()=//for pagination
            //ToPagedList=list which loads data from data source and accepts 2 parameters -page nuber, page size
            return View(yy);
          

        }//go to Home.cshtml and type the code for pagination 

     

        [HttpGet]  //actionVerbs
        [Authorize(Roles = "Chemist")]
        public ActionResult Create()
        {
            Store ss = new Store();  //creating object
            return View(ss);  //passing it
        }

        [HttpPost]  //actionVerbs
        [Authorize(Roles = "Chemist")]
        public ActionResult Create(StoreView x) //model binding concept=creating parameters object
      
        {
            if (ModelState.IsValid)
            {
                //code for storing image in db after uploading
                HttpPostedFileBase Photo = Request.Files["MedicinePhtot"];
                //     HttpPostedFileBase: This is the easiest way to read the uploaded files into the controller 
                if (Photo !=null)
                    try
                    {
                        var fname = Path.GetFileName(Photo.FileName);
                        var p = Path.Combine(Server.MapPath("~/Image/"), fname);
                        Photo.SaveAs(p);
                        x.MedicinePhtot = p;
                    }
                    catch
                    {

                    }
                     Store s = new Store
                     {
                         MedicineName = x.MedicineName,
                         PharmEmail = x.PharmEmail,
                         PharmNumber = x.PharmNumber,
                         PharmWebsite = x.PharmWebsite,
                         ExpiryData = x.ExpiryData,
                         ManufacturedDate = x.ManufacturedDate,
                         price = x.price,
                         MedicinePhtot=x.MedicinePhtot

                     };    
                db.Stores.Add(s);
                db.SaveChanges();
                return RedirectToAction("Home");
                
            }
            return View(x);
        }

          //details
            [HttpGet]
        [Authorize(Roles = "Chemist")]
        public ActionResult Details(int id) //model binding=mapping through id
        {
            var y = db.Stores.FirstOrDefault(x => x.Id == id);
            return View(y);
        }

        //delete
        [HttpGet]
        [Authorize(Roles = "Chemist")]
        public ActionResult Delete(int? id)  //same parameter, method name for both get and post so make the id as nullable
        {
            var d = db.Stores.FirstOrDefault(x => x.Id == id);
          
            return View(d);
        }

        [HttpPost]
        [Authorize(Roles = "Chemist")]
        public ActionResult Delete(int id)
        {
            var r = db.Stores.FirstOrDefault(x => x.Id == id);
            db.Stores.Remove(r);
            db.SaveChanges();
            return RedirectToAction("Home");
        }

        //update
        [HttpGet]
        [Authorize(Roles = "Chemist")]
        public ActionResult Edit(int id)
        {
            var u = db.Stores.FirstOrDefault(x => x.Id == id);
           
            return View(u);
        }

        [HttpPost]
        [Authorize(Roles = "Chemist")]
        public ActionResult Edit(FormCollection values)  //model binding=formcollection to get data values 
        {
            //formCollection=to retrive all values from view form fields ,this is the reason we use for EDIT
            var y = Convert.ToInt32(values["Id"]);
            var u = db.Stores.FirstOrDefault(x => x.Id == y);
            //for fetching values from data
         
             u.MedicineName = values["MedicineName"];
            u.ExpiryData =Convert.ToDateTime(values["ExpiryData"]);
            u.ManufacturedDate =Convert.ToDateTime(values["ManufacturedDate"]);
            u.PharmEmail =Convert.ToString(values["PharmEmail"]);
            u.PharmNumber = values["PharmNumber"];
            u.PharmWebsite = values["PharmWebsite"];
            u.price =Convert.ToDouble(values["price"]);
            
            db.SaveChanges();
         return   RedirectToAction("Home");
           
        }
      

    }
}