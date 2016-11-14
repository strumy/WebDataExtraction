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
    public class SearchDatasController : Controller
    {
        private SearchContext db = new SearchContext();

        // GET: SearchDatas
        public ActionResult Index()
        {
            return View(db.SearchDatas.ToList());
        }

        // GET: SearchDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchData searchData = db.SearchDatas.Find(id);
            if (searchData == null)
            {
                return HttpNotFound();
            }
            return View(searchData);
        }

        // GET: SearchDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SearchDataId,Name,Location")] SearchData searchData)
        {
            if (ModelState.IsValid)
            {
                db.SearchDatas.Add(searchData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(searchData);
        }

        // GET: SearchDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchData searchData = db.SearchDatas.Find(id);
            if (searchData == null)
            {
                return HttpNotFound();
            }
            return View(searchData);
        }

        // POST: SearchDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SearchDataId,Name,Location")] SearchData searchData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(searchData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(searchData);
        }

        // GET: SearchDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchData searchData = db.SearchDatas.Find(id);
            if (searchData == null)
            {
                return HttpNotFound();
            }
            return View(searchData);
        }

        // POST: SearchDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SearchData searchData = db.SearchDatas.Find(id);
            db.SearchDatas.Remove(searchData);
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
