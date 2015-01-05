using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcAutomation.Models;
using MvcAutomation.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcAutomation.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IFacultyService facultyService;
        private readonly ISpecialityService specialityService;
        private readonly ICourseService courseService;
        private readonly IGroupService groupService;

        public RegistrationController(IUserService service1, IRoleService service2, IFacultyService service3,
            ISpecialityService service4, ICourseService service5, IGroupService service6)
        {
            userService = service1;
            roleService = service2;
            facultyService = service3;
            specialityService = service4;
            courseService = service5;
            groupService = service6;
        }

        [HttpGet]
        public ActionResult StepOne()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StepOne(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                MembershipUser memberUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(user.Nickname, user.FirstName, user.LastName, user.Password, user.Email,
                    null, null, null, null, roleService.GetAllRoleEntities().FirstOrDefault(ent => ent.Name == "User").Id);
                if (memberUser != null)
                {
                    var userEnt = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == user.Nickname);
                    if (userEnt != null)
                    {
                        Response.Cookies["user_name"].Value = userEnt.FirstName;
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

            foreach (FacultyEntity fe in facultyService.GetAllFacultyEntities())
                faculties.Add(new SelectListItem() { Text = fe.Name });

            List<SelectListItem> groups = new List<SelectListItem>();

            foreach (GroupEntity ge in groupService.GetAllGroupEntities())
                groups.Add(new SelectListItem() { Text = ge.Name });

            List<SelectListItem> courses = new List<SelectListItem>();

            foreach (CourseEntity ce in courseService.GetAllCourseEntities())
                courses.Add(new SelectListItem() { Text = ce.Number.ToString() });

            List<SelectListItem> specialities = new List<SelectListItem>();

            foreach (SpecialityEntity se in specialityService.GetAllSpecialityEntities())
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
                facultyService.GetAllFacultyEntities().FirstOrDefault(ent => ent.Name == information.Faculty).Id,
                specialityService.GetAllSpecialityEntities().FirstOrDefault(ent => ent.Name == information.Speciality).Id,
                courseService.GetAllCourseEntities().FirstOrDefault(ent => ent.Number == Int32.Parse(information.Course)).Id,
                groupService.GetAllGroupEntities().FirstOrDefault(ent => ent.Name == information.Group).Id);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public JsonResult CheckNickName(string nickName)
        {
            bool result = Membership.FindUsersByName(nickName).Count == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
