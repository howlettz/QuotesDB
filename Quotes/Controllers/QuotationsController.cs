using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quotes.Models;
using System.Web.UI;

namespace Quotes.Controllers
{
    public class QuotationsController : Controller
    {
        private QuotesContext db = new QuotesContext();
        private VarHolder storeSearch = new VarHolder();


        // GET: Quotations
        public ActionResult Index(string searchTerm)
        {

            storeSearch.isSearch = false;

            var quotations = db.Quotations.Include(q => q.Category);

            var searchQuotes = db.Quotations.AsQueryable();

            if (searchTerm != null && searchTerm != "")
            {
                var quoteSearch = searchQuotes.Where(g => g.Quote.Contains(searchTerm));
                var authorSearch = searchQuotes.Where(g => g.Author.Contains(searchTerm));
                var categorySearch = searchQuotes.Where(g => g.Category.Name.Contains(searchTerm));

                searchQuotes = quoteSearch.Union(authorSearch);
                searchQuotes = searchQuotes.Union(categorySearch);

                searchQuotes = searchQuotes.Distinct();

                storeSearch.isSearch = true;
            }

            ViewBag.isSearch = storeSearch.isSearch;

            return View(searchQuotes);
        }

        // GET: Quotations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotations quotations = db.Quotations.Find(id);
            if (quotations == null)
            {
                return HttpNotFound();
            }
            return View(quotations);
        }

        // GET: Quotations/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]                                 //DateAdded
        public ActionResult Create([Bind(Include = "ID,Author,Quote,CategoryID")] Quotations quotations, string CategoryName)
        {
            //Put current time into DateAdded
            quotations.DateAdded = DateTime.Now;

            //If there is a category name entered
            if (CategoryName != "")
            {
                //Check for a category with that name first to avoid duplicate
                if (db.Categories.Count(s => s.Name.Equals(CategoryName)) == 0)
                {
                    //Since the category does not yet exist, add it!
                    Category newCat = new Category();

                    newCat.Name = CategoryName;
                    db.Categories.Add(newCat);
                    db.SaveChanges();
                    quotations.CategoryID = (int)newCat.ID;
                }
                else
                {
                    //Since the category does exist, match the category in the text box with one in the database
                    var testing = db.Categories.Single(g => g.Name.Equals(CategoryName));
                    quotations.CategoryID = (int)testing.ID;
                }

            }

            if (ModelState.IsValid)
            {
                db.Quotations.Add(quotations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", quotations.CategoryID);
            return View(quotations);
        }

        // GET: Quotations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotations quotations = db.Quotations.Find(id);
            if (quotations == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", quotations.CategoryID);
            return View(quotations);
        }

        // POST: Quotations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Author,Quote,DateAdded,CategoryID")] Quotations quotations, string CategoryName)
        {

            //If there is a category name entered
            if (CategoryName != "")
            {
                //Check for a category with that name first to avoid duplicate
                if (db.Categories.Count(s => s.Name.Equals(CategoryName)) == 0)
                {
                    //Since the category does not yet exist, add it!
                    Category newCat = new Category();

                    newCat.Name = CategoryName;
                    db.Categories.Add(newCat);
                    db.SaveChanges();
                    quotations.CategoryID = (int)newCat.ID;
                }
                else
                {
                    //Since the category does exist, match the category in the text box with one in the database
                    var testing = db.Categories.Single(g => g.Name.Equals(CategoryName));
                    quotations.CategoryID = (int)testing.ID;

                }

            }

            if (ModelState.IsValid)
            {
                db.Entry(quotations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", quotations.CategoryID);
            return View(quotations);
        }

        // GET: Quotations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotations quotations = db.Quotations.Find(id);
            if (quotations == null)
            {
                return HttpNotFound();
            }
            return View(quotations);
        }

        // POST: Quotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotations quotations = db.Quotations.Find(id);
            db.Quotations.Remove(quotations);
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
