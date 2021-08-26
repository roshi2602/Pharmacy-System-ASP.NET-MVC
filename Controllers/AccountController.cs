using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practice4.Models;
using System.Web.Security;
using Practice4.ViewModel;
namespace Practice4.Controllers
{
    public class AccountController : Controller
    {
        PracticeaspDBEntities1 db = new PracticeaspDBEntities1();
        //create 1 boolean method to check user credentials
        public bool CredCheck(Account au)
        {
            //create var to check for username and password
            var x = db.Accounts.FirstOrDefault(i => i.Username == au.Username && i.Password == au.Password);

            //apply condition for DB valid username and password
            if (x != null)
            {
                return (au.Username == x.Username && au.Password == x.Password);
            }
            else
            {
                return false;
            }
        }

        //create method for login get and post
        [HttpGet]
        public ActionResult Login()
        {
            AccountView ac = new AccountView();
            return View(ac);
        }

        [HttpPost]
        public ActionResult Login(Account au, string myreturnurl) //model binding
        {
            //now using CredCheck method made above for au to get stored in that
            if (CredCheck(au))
            {
                FormsAuthentication.SetAuthCookie(au.Username, false);//false for creating temporary cookie
                                                                      //FormsAuthentication manages authentication
                                                                      //setAuthCookie created authentication ticket for supplied username
                Session["username"] = au.Username.ToString();   //Session named username is equal to model.username
                
          
                
                //  myreturnurl=is for authentication over every url we visit
                if (myreturnurl != null)
                {
                    return Redirect(myreturnurl); //redirect to same
                }
                else
                {
                    TempData["msg"] = "<script>alert('welcome');</script>";  //alertbox //tempdata paases from controller to view in dictionary format -key=value
                    //its value is accessed in Home.cshtml
                    return RedirectToAction("Home", "Store");
                    //myreturnurl=due to this we will get authentication at every url where we have applied [Authorize]

                }
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); //signout 
            Session["username"] = null; //destroy the credentials for that session
            
            return RedirectToAction("Login", "Account");
        }

        //method for signup
        [HttpGet]
        public ActionResult SignUp()
        {
            AccountView aa = new AccountView();
            return View(aa);
        }

        [HttpPost]
        public ActionResult SignUp(AccountView x) //model binding
        {
            Account aa = new Account
            {
                
                Username=x.Username,
                Password=x.Password,
                ConfirmPassword=x.ConfirmPassword
            };
            db.Accounts.Add(aa);
            db.SaveChanges();
            return RedirectToAction("Login");
        }


    }
}