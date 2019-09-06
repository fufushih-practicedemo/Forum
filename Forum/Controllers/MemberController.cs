using Forum.Models.Data;
using Forum.Models.ViewModel.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Forum.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        // GET: Member/register
        public ActionResult Register()
        {
            return View("register");
        }

        // POST Member/register
        [HttpPost]
        public ActionResult Register(MemberVM model)
        {
            if (!ModelState.IsValid) {
                return View("register", model);
            }

            if (!model.Password.Equals(model.ConfirmPassword)) {
                ModelState.AddModelError("", "密碼不一致");
                return View("register", model);
            }

            using (Db db = new Db()) {
                if (db.Members.Any(x => x.Account.Equals(model.Account))) {
                    ModelState.AddModelError("", "Account" + model.Account + "is taken!");
                    model.Account = "";
                    return View("register", model);
                }

                MemberDTO memberDTO = new MemberDTO()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Account = model.Account,
                    Password = model.Password
                };

                db.Members.Add(memberDTO);
                db.SaveChanges();
            }

            return Redirect("~/member/login");

        }

        // GET: /Member/Login
        public ActionResult Login()
        {
            string account = User.Identity.Name;

            if (!string.IsNullOrEmpty(account)) {
                return RedirectToAction("member-profile");
            }

            return View();
        }

        // POST: /Member/Login
        [HttpPost]
        public ActionResult Login(MemberLoginVM model)
        {
            if (!ModelState.IsValid) {
                return View(model);
            }

            bool isValid = false;

            using (Db db = new Db()) {
                if (db.Members.Any(x => x.Account.Equals(model.UserName) && x.Password.Equals(model.Password))) {
                    isValid = true;
                }

                if (!isValid) {
                    ModelState.AddModelError("", "帳號或密碼錯誤");
                    return View(model);
                } else {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Redirect(FormsAuthentication.GetRedirectUrl(model.UserName, model.RememberMe));
                }
            }
        }

        // GET: /Member/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Member/Login");
        }

        // GET: /Member/member-profile
        [ActionName("member-profile")]
        [HttpGet]
        public ActionResult MemberProfile()
        {
            string Account = User.Identity.Name;

            MemberProfileVM model;

            using (Db db = new Db()) {
                // get user
                MemberDTO dto = db.Members.FirstOrDefault(x => x.Account == Account);

                model = new MemberProfileVM(dto);
            }

            return View("MemberProfile", model);
        }

        // POST: /Member/member-profile
        [HttpPost]
        [ActionName("member-profile")]
        public ActionResult MemberProfile(MemberProfileVM model)
        {
            if (!ModelState.IsValid) {
                return View("MemberProfile", model);
            }

            if (!string.IsNullOrWhiteSpace(model.Password)) {
                if (!model.Password.Equals(model.ConfirmPassword)) {
                    ModelState.AddModelError("", "密碼不符");
                    return View("MemberProfile", model);
                }
            }

            using (Db db = new Db()) {
                string account = User.Identity.Name;

                // Check account is unique
                if (db.Members.Where(x => x.UID != model.UID).Any(x => x.Account == account)) {
                    ModelState.AddModelError("", "帳號 " + model.Account + " 已存在!");
                    model.Account = "";
                    return View("MemberProfile", model);
                }

                // Edit
                MemberDTO dto = db.Members.Find(model.UID);

                dto.Name = model.Name;
                dto.Email = model.Email;
                dto.Account = model.Account;
                if (!string.IsNullOrWhiteSpace(model.Password)) {
                    dto.Password = model.Password;
                }

                db.SaveChanges();
            }

            return Redirect("~/member/MemberProfile");
        }
    }
}