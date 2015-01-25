using BLL.Interface.Entities;
using BLL.Interface.Services;
using CaptchaLibrary;
using MvcAutomation.Models;
using MvcAutomation.Providers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcAutomation.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService userService;

        public RegistrationController(IUserService service)
        {
            userService = service;
        }

        public ActionResult Captcha()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            StringBuilder sb = new StringBuilder(rand.Next(1111, 9999).ToString());
            for (int i = 0; i != 4; i++)
                sb.Append((char)(rand.Next(26) + (int)'a'));
            string code = sb.ToString();

            Session["code"] = code;
            CaptchaImage captcha = new CaptchaImage(code, 180, 50);

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            captcha.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            captcha.Dispose();
            return null;
        }

        [HttpGet]
        public ActionResult StepOne()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StepOne(UserViewModel user)
        {
            if (user.Captcha != (string)Session["code"])
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
            }
            if (ModelState.IsValid)
            {
                MembershipUser memberUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(user.Nickname, user.FirstName, user.LastName, user.Password, user.Email,
                    null, null, null, null, userService.GetAllRoleEntities().FirstOrDefault(ent => ent.Name == "User").Id);
                if (memberUser != null)
                {
                    var userEnt = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == user.Nickname);
                    if (userEnt != null)
                    {
                        Response.Cookies["user_name"].Value = Convert.ToBase64String(Encoding.Default.GetBytes(userEnt.FirstName));
                        Response.Cookies["user_name"].Expires = DateTime.Now.AddDays(2);
                    }
                    FormsAuthentication.SetAuthCookie(user.Nickname, true);
                    return RedirectToAction("StepTwo", "Registration");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult StepTwo()
        {
            List<SelectListItem> faculties = new List<SelectListItem>();

            foreach (FacultyEntity fe in userService.GetAllFacultyEntities())
                faculties.Add(new SelectListItem() { Text = fe.Name });

            List<SelectListItem> groups = new List<SelectListItem>();

            foreach (GroupEntity ge in userService.GetAllGroupEntities())
                groups.Add(new SelectListItem() { Text = ge.Name });

            List<SelectListItem> courses = new List<SelectListItem>();

            foreach (CourseEntity ce in userService.GetAllCourseEntities())
                courses.Add(new SelectListItem() { Text = ce.Number.ToString() });

            List<SelectListItem> specialities = new List<SelectListItem>();

            foreach (SpecialityEntity se in userService.GetAllSpecialityEntities())
                specialities.Add(new SelectListItem() { Text = se.Name });

            ViewBag.Faculties = faculties;
            ViewBag.Courses = courses;
            ViewBag.Groups = groups;
            ViewBag.Specialities = specialities;

            return View();
        }

        [HttpPost]
        public ActionResult StepTwo(InformationViewModel information)
        {
            ((CustomMembershipProvider)Membership.Provider).UpdateUser(User.Identity.Name,
                userService.GetAllFacultyEntities().FirstOrDefault(ent => ent.Name == information.Faculty).Id,
                userService.GetAllSpecialityEntities().FirstOrDefault(ent => ent.Name == information.Speciality).Id,
                userService.GetAllCourseEntities().FirstOrDefault(ent => ent.Number == Int32.Parse(information.Course)).Id,
                userService.GetAllGroupEntities().FirstOrDefault(ent => ent.Name == information.Group).Id);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public JsonResult CheckNickName(string nickName)
        {
            bool result = Membership.FindUsersByName(nickName).Count == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }
    }
}
