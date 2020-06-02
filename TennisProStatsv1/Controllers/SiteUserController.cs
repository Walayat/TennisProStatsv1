using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TennisProStatsv1.Models;
using TennisProStatsv1.Utilities;

namespace TennisProStatsv1.Controllers
{
    public class SiteUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SiteUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerModel)
        {
            User user = new Models.User();

            var emailExist = await db.SitUsers.Where(p => p.Email.Equals(registerModel.Email)).FirstOrDefaultAsync();

            if (!(emailExist is null))
            {
                ViewBag.note = "Enter Different Email";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    user.Email = registerModel.Email;
                    user.Password = registerModel.Password;
                    user.UserTypeId = (int) CommonThings.UserType.SiteUser;
                    user.IsValid = false;
                    db.SitUsers.Add(user);
                    await db.SaveChangesAsync();

                    ViewBag.success = true;
                    CommonThings.BuildEmailTemplate(user.ID,user.Email);

                    Session["Email"] = user.Email;
                    Session["UserTypeId"] = user.UserTypeId;
                }
            }

            return View(registerModel);
        }

        public ActionResult Confirm(int userId)
        {
            ViewBag.userID = userId;
            return View();
        }

        public JsonResult RegisterConfirm(int userId)
        {
            var userData = db.SitUsers.Where(x => x.ID == userId).FirstOrDefault();
            userData.IsValid = true;
            db.SaveChanges();
            var msg = "Your email is verified";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var checkCredentials = await db.SitUsers.Where(x => x.Email == loginModel.Email && x.Password == loginModel.Password).SingleOrDefaultAsync();

                if (!(checkCredentials is null))
                {
                    Session["Email"] = checkCredentials.Email;
                    Session["UserTypeId"] = checkCredentials.UserTypeId;
                    return Redirect("~/Home/Dashboard");
                }
                else
                {
                    ViewBag.InvalidAttempt = "Invalid email or password";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("Password", "Something went wrong with credentials");
            }
            return View(loginModel);
        }

        public bool islogin()
        {
            if (Session["Email"] == null)
            {
                return true;
            }
            return false;
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("~/SiteUser/Login");
        }

    }
}