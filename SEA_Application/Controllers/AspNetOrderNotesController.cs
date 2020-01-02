﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using SEA_Application.Models;

namespace SEA_Application.Controllers
{
    public class AspNetOrderNotesController : Controller
    {
        private SEA_DatabaseEntities db = new SEA_DatabaseEntities();

        // GET: AspNetOrderNotes
        public ActionResult Index()
        {
            var aspNetNotes = db.AspNetNotes.Include(a => a.AspNetSubject);
            return View(aspNetNotes.ToList());
        }

        // GET: AspNetOrderNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetNote aspNetNote = db.AspNetNotes.Find(id);
            if (aspNetNote == null)
            {
                return HttpNotFound();
            }
            return View(aspNetNote);
        }
        public ActionResult RecentOrders()
        {
            var CurrentUserId = User.Identity.GetUserId();

            int StudentId = db.AspNetStudents.Where(x => x.StudentID == CurrentUserId).FirstOrDefault().Id;

            var result = from Notes in db.AspNetNotes
                         join OrderNotes in db.AspNetNotesOrders on Notes.Id equals OrderNotes.NotesID
                         where OrderNotes.StudentID == StudentId
                         select new
                         {
                             Id = OrderNotes.Id,
                             Title = Notes.Title,
                             Discription = Notes.Description,
                             CourseType = Notes.CourseType,
                             Price = Notes.Price,
                             Quantity = OrderNotes.Quantity,
                             Status = OrderNotes.Status,

                         };
                
      


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelOrder(int OrderId)
        {

          AspNetNotesOrder NotesOrder=   db.AspNetNotesOrders.Where(x => x.Id == OrderId).FirstOrDefault();
            NotesOrder.Status = "Cancelled";

            db.Entry(NotesOrder).State = EntityState.Modified;

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }



            public ActionResult ConfirmOrder(int NotesId, int Quantity)
        {

            var CurrentUserId = User.Identity.GetUserId();

            int StudentId = db.AspNetStudents.Where(x => x.StudentID == CurrentUserId).FirstOrDefault().Id;


            AspNetNotesOrder NotesOrder = new AspNetNotesOrder();

            NotesOrder.NotesID = NotesId;
            NotesOrder.StudentID = StudentId;

            NotesOrder.Quantity = Quantity;
            NotesOrder.Status = "Draft";

            db.AspNetNotesOrders.Add(NotesOrder);


            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: AspNetOrderNotes/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.AspNetSubjects, "Id", "SubjectName");
            return View();
        }

        // POST: AspNetOrderNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,SubjectID,CourseType,Price,CreationDate")] AspNetNote aspNetNote)
        {
            if (ModelState.IsValid)
            {
                db.AspNetNotes.Add(aspNetNote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.AspNetSubjects, "Id", "SubjectName", aspNetNote.SubjectID);
            return View(aspNetNote);
        }

        // GET: AspNetOrderNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetNote aspNetNote = db.AspNetNotes.Find(id);
            if (aspNetNote == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.AspNetSubjects, "Id", "SubjectName", aspNetNote.SubjectID);
            return View(aspNetNote);
        }

        // POST: AspNetOrderNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,SubjectID,CourseType,Price,CreationDate")] AspNetNote aspNetNote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.AspNetSubjects, "Id", "SubjectName", aspNetNote.SubjectID);
            return View(aspNetNote);
        }

        // GET: AspNetOrderNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetNote aspNetNote = db.AspNetNotes.Find(id);
            if (aspNetNote == null)
            {
                return HttpNotFound();
            }
            return View(aspNetNote);
        }

        // POST: AspNetOrderNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetNote aspNetNote = db.AspNetNotes.Find(id);
            db.AspNetNotes.Remove(aspNetNote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}