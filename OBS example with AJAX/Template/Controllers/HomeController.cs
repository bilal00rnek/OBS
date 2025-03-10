using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Models;
namespace Template.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        OBSEntities obs = new OBSEntities();
        public ActionResult Index()
        {
            string studentNumber = Session["studentNumber"].ToString();
            List<Selection> selections = obs.Selections.Where(a=>a.StudentNumber.Equals(studentNumber)).ToList();
            return View(selections);
        }
        [HttpGet]
        public JsonResult getLectures()
        {
            return Json(obs.Lectures.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string CheckLogin(string studentNumber, string password)
        {
            Student loginUser = obs.Students.Where(a => a.StudentNumber.Equals(studentNumber) && a.StudentPassword.Equals(password)).FirstOrDefault();
            if (loginUser != null)
            {
                Session["studentNumber"] = studentNumber;
                return "success";
            }
            else
                return "error";
        }
        [HttpPost]
        public string ValidateAdding(string CourseCode)
        {
            string studentNumber = Session["studentNumber"].ToString();
            Selection selection = obs.Selections.Where(a => a.StudentNumber.Equals(studentNumber) && a.CourseCode.Equals(CourseCode)).FirstOrDefault();
            if (selection != null)
                return "This course is already selected.";
            else
                return "success";
        }
        [HttpPost]
        public ActionResult Add(string CourseCode)
        {
                Selection newSelection = new Selection();
                newSelection.CourseCode = CourseCode;
                newSelection.StudentNumber = Session["studentNumber"].ToString();
                obs.Selections.Add(newSelection);
                obs.SaveChanges();
                return RedirectToAction("Index");
        }
        public ActionResult Remove(int SelectionID)
        {
            Selection deleted = obs.Selections.Find(SelectionID);
            obs.Selections.Remove(deleted);
            obs.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}