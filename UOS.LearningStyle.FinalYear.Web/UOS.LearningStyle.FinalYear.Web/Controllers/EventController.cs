using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Core.Extensions;
using UOS.LearningStyle.FinalYear.Core.Loggings;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IAppointmentDiaryService _appointmentDiaryService;
        private string UserId;
        private readonly FileLogger Logger;
        public EventController(
            IAppointmentDiaryService appointmentDiaryService)
        {
            _appointmentDiaryService = appointmentDiaryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Event
        public JsonResult GetDiaryEvents(DateTime start, DateTime end)
        {
            UserId = User.Identity.Name;

            try
            {
                var appointmentDiaries = _appointmentDiaryService.RetrieveAppointmentDiaries(UserId);

                return Json(LoadAllAppointmentsInDateRange(start, end).Select(x => new
                {
                    id = x.ID,
                    title = x.Title,
                    start = x.StartDateString,
                    end = x.EndDateString,
                    color = x.StatusColor,
                    className = x.ClassName,
                    someKey = x.SomeImportantKeyID,
                    allDay = false
                }).ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                FileLogger.Error(ex.Message);

                return Json(null);
            }
        }

        [NonAction]
        public IEnumerable<DiaryEvent> LoadAllAppointmentsInDateRange(DateTime fromDate, DateTime toDate)
        {
            List<DiaryEvent> result = new List<DiaryEvent>();
            var appointmentDiaries = _appointmentDiaryService.RetrieveAppointmentDiaries(UserId);
            foreach (var item in appointmentDiaries.Where(s => s.DateTimeScheduled >= fromDate && s.DateTimeScheduled.AddMinutes(s.AppointmentLength) <= toDate))
            {
                DiaryEvent rec = new DiaryEvent
                {
                    ID = item.ID,
                    SomeImportantKeyID = item.SomeImportantKey,
                    StartDateString = item.DateTimeScheduled.ToString("s"), // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                    EndDateString = item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s"), // field AppointmentLength is in minutes
                    Title = item.Title + " - " + item.AppointmentLength.ToString() + " mins",
                    StatusString = Enums.GetName((AppointmentStatus)item.StatusENUM)
                };
                rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                rec.StatusColor = ColorCode;
                result.Add(rec);
            }
            return result;
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            UserId = User.Identity.Name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appointmentDiary = _appointmentDiaryService.RetrieveAppointmentDiary((int)id, UserId);

            if (appointmentDiary == null)
            {
                return HttpNotFound();
            }

            return View(appointmentDiary);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            UserId = User.Identity.Name;
            ViewBag.ID = new SelectList(_appointmentDiaryService.RetrieveAppointmentDiaries(UserId), "ID", "Title");

            ViewBag.Hours = new SelectList(Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("hh:mm tt")), "Hours");

            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SomeImportantKey,Title,StatusENUM,DateTimeScheduled,AppointmentLength,ID")] AppointmentDiary appointmentDiary, string Hours)
        {
            appointmentDiary.UserId = UserId = User.Identity.Name;

            var dt = string.Concat(appointmentDiary.DateTimeScheduled.ToShortDateString(), " ", Hours);

            var dateTime = DateTime.ParseExact(
                dt,
                "dd/MM/yyyy hh:mm tt",
                CultureInfo.InvariantCulture
            );

            appointmentDiary.DateTimeScheduled = dateTime;

            if (ModelState.IsValid)
            {
                _appointmentDiaryService.AddAppointmentDiary(appointmentDiary);

                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(_appointmentDiaryService.RetrieveAppointmentDiaries(UserId), "ID", "Title", appointmentDiary.ID);

            ViewBag.Hours = new SelectList(Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("hh:mm tt")), "Hours");
            return View(appointmentDiary);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            UserId = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appointmentDiary = _appointmentDiaryService.RetrieveAppointmentDiary((int)id, UserId);
            if (appointmentDiary == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(_appointmentDiaryService.RetrieveAppointmentDiaries(UserId), "ID", "Title", appointmentDiary.ID);

            ViewBag.Hours = new SelectList(Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("hh:mm tt")), "Hours");

            return View(appointmentDiary);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SomeImportantKey,Title,StatusENUM,DateTimeScheduled,AppointmentLength,ID")] AppointmentDiary appointmentDiary, string Hours)
        {
            appointmentDiary.UserId = UserId = User.Identity.Name;

            var dt = string.Concat(appointmentDiary.DateTimeScheduled.ToShortDateString(), " ", Hours);

            var dateTime = DateTime.ParseExact(
                dt,
                "dd/MM/yyyy hh:mm tt",
                CultureInfo.InvariantCulture
            );

            appointmentDiary.DateTimeScheduled = dateTime;

            if (ModelState.IsValid)
            {
                //_appointmentDiaryService.u
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(_appointmentDiaryService.RetrieveAppointmentDiaries(UserId), "ID", "Title", appointmentDiary.ID);

            ViewBag.Hours = new SelectList(Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("hh:mm tt")), "Hours");

            return View(appointmentDiary);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            UserId = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseGrade = _appointmentDiaryService.RetrieveAppointmentDiary((int)id, UserId);

            if (courseGrade == null)
            {
                return HttpNotFound();
            }
            return View(courseGrade);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserId = User.Identity.Name;
            AppointmentDiary appointmentDiary = _appointmentDiaryService.RetrieveAppointmentDiary(id, UserId);

            _appointmentDiaryService.RemoveAppointmentDiary(appointmentDiary);

            return RedirectToAction("Index");
        }
    }
}