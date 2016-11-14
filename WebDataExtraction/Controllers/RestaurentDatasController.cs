using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDataExtraction.Context;
using WebDataExtraction.Entity;

namespace WebDataExtraction.Controllers
{
    public class RestaurentDatasController : Controller
    {
        private SearchContext db = new SearchContext();

        // GET: RestaurentDatas
        public ActionResult Index()
        {
            var restaurentDatas = db.RestaurentDatas.Include(r => r.SearchData);
            return View(restaurentDatas.ToList());
        }

        // GET: RestaurentDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurentData restaurentData = db.RestaurentDatas.Find(id);
            if (restaurentData == null)
            {
                return HttpNotFound();
            }
            return View(restaurentData);
        }

        // GET: RestaurentDatas/Create
        public ActionResult Create()
        {
            ViewBag.SearchDataId = new SelectList(db.SearchDatas, "SearchDataId", "Name");
            return View();
        }

        // POST: RestaurentDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurentId,Name,Address,Zip,SearchDataId")] RestaurentData restaurentData)
        {
            if (ModelState.IsValid)
            {
                db.RestaurentDatas.Add(restaurentData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SearchDataId = new SelectList(db.SearchDatas, "SearchDataId", "Name", restaurentData.SearchDataId);
            return View(restaurentData);
        }

        // GET: RestaurentDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurentData restaurentData = db.RestaurentDatas.Find(id);
            if (restaurentData == null)
            {
                return HttpNotFound();
            }
            ViewBag.SearchDataId = new SelectList(db.SearchDatas, "SearchDataId", "Name", restaurentData.SearchDataId);
            return View(restaurentData);
        }

        // POST: RestaurentDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestaurentId,Name,Address,Zip,SearchDataId")] RestaurentData restaurentData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurentData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SearchDataId = new SelectList(db.SearchDatas, "SearchDataId", "Name", restaurentData.SearchDataId);
            return View(restaurentData);
        }

        // GET: RestaurentDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurentData restaurentData = db.RestaurentDatas.Find(id);
            if (restaurentData == null)
            {
                return HttpNotFound();
            }
            return View(restaurentData);
        }

        // POST: RestaurentDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurentData restaurentData = db.RestaurentDatas.Find(id);
            db.RestaurentDatas.Remove(restaurentData);
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
