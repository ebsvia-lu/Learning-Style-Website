using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Web.Controllers
{
    public class CourseGradeController : Controller
    {
        private readonly ICourseGradeService _courseGradeService;
        private readonly ICalculatorService _calculatorervice;
        private string UserId;
        public CourseGradeController(
            ICourseGradeService courseGradeService,
            ICalculatorService calculatorervice)
        {
            _courseGradeService = courseGradeService;
            _calculatorervice = calculatorervice;
        }

        // GET: CourseGrade
        public ActionResult Index()
        {
            UserId = User.Identity.Name;

            var courseGrades = _courseGradeService.RetrieveCourseGrades(UserId);

            if (courseGrades != null && courseGrades.Count() > 0)
            {
                _calculatorervice.CourseGrades = courseGrades;

                ViewBag.Total = _calculatorervice.CalculateTotal();

                ViewBag.BasicAverage = _calculatorervice.CalculateBasicAverage();

                ViewBag.WeightedAverage = _calculatorervice.CalculateWeightedAverage(); 
            }

            return View(courseGrades.ToList());
        }

        // GET: CourseGrade/Details/5
        public ActionResult Details(int? id)
        {
            UserId = User.Identity.Name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseGrade = _courseGradeService.RetrieveCourseGrade((int)id, UserId);

            if (courseGrade == null)
            {
                return HttpNotFound();
            }

            return View(courseGrade);
        }

        // GET: CourseGrade/Create
        public ActionResult Create()
        {
            UserId = User.Identity.Name;
            ViewBag.ID = new SelectList(_courseGradeService.RetrieveCourseGrades(UserId), "ID", "Title");
            return View();
        }

        // POST: CourseGrade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Number,Title,Mark,Credits,ID")] CourseGrade courseGrade)
        {
            courseGrade.UserId = UserId = User.Identity.Name;
            //checks if valid
            if (ModelState.IsValid)
            {
                _courseGradeService.AddCourseGrade(courseGrade);
                
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(_courseGradeService.RetrieveCourseGrades(UserId), "ID", "Title", courseGrade.ID);
            return View(courseGrade);
        }

        // GET: CourseGrade/Edit/5
        public ActionResult Edit(int? id)
        {
            UserId = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseGrade = _courseGradeService.RetrieveCourseGrade((int)id, UserId);
            if (courseGrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(_courseGradeService.RetrieveCourseGrades(UserId), "ID", "Title", courseGrade.ID);
            return View(courseGrade);
        }

        // POST: CourseGrade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Number,Title,Mark,Credits,ID")] CourseGrade courseGrade)
        {
            courseGrade.UserId = UserId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _courseGradeService.UpdateCourseGrade(courseGrade);
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(_courseGradeService.RetrieveCourseGrades(UserId), "ID", "Title", courseGrade.ID);
            return View(courseGrade);
        }

        // GET: CourseGrade/Delete/5
        public ActionResult Delete(int? id)
        {
            UserId = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseGrade = _courseGradeService.RetrieveCourseGrade((int)id, UserId);

            if (courseGrade == null)
            {
                return HttpNotFound();
            }
            return View(courseGrade);
        }

        // POST: CourseGrade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserId = User.Identity.Name;
            CourseGrade courseGrade = _courseGradeService.RetrieveCourseGrade(id, UserId);

            _courseGradeService.RemoveCourseGrade(courseGrade);

            return RedirectToAction("Index");
        }
    }
}