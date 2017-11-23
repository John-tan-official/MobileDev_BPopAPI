using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BPopAPI.Models;

namespace BPopAPI.Controllers
{
    public class QandAController : Controller
    {
        private BPopAPIContext db = new BPopAPIContext();

        // GET: QandA
        public async Task<ActionResult> Index()
        {
            return View(await db.QandAs.ToListAsync());
        }

        // GET: QandA/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QandA qandA = await db.QandAs.FindAsync(id);
            if (qandA == null)
            {
                return HttpNotFound();
            }
            return View(qandA);
        }

        // GET: QandA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QandA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Question,ChoiceA,ChoiceB,ChoiceC,ChoiceD,Answer")] QandA qandA)
        {
            if (ModelState.IsValid)
            {
                db.QandAs.Add(qandA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(qandA);
        }

        // GET: QandA/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QandA qandA = await db.QandAs.FindAsync(id);
            if (qandA == null)
            {
                return HttpNotFound();
            }
            return View(qandA);
        }

        // POST: QandA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Question,ChoiceA,ChoiceB,ChoiceC,ChoiceD,Answer")] QandA qandA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qandA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qandA);
        }

        // GET: QandA/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QandA qandA = await db.QandAs.FindAsync(id);
            if (qandA == null)
            {
                return HttpNotFound();
            }
            return View(qandA);
        }

        // POST: QandA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QandA qandA = await db.QandAs.FindAsync(id);
            db.QandAs.Remove(qandA);
            await db.SaveChangesAsync();
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
