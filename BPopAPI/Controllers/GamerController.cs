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
    public class GamerController : Controller
    {
        private BPopAPIContext db = new BPopAPIContext();

        // GET: Gamer
        public async Task<ActionResult> Index()
        {
            return View(await db.GameStarters.ToListAsync());
        }

        // GET: Gamer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameStarter gameStarter = await db.GameStarters.FindAsync(id);
            if (gameStarter == null)
            {
                return HttpNotFound();
            }
            return View(gameStarter);
        }

        // GET: Gamer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gamer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,begin")] GameStarter gameStarter)
        {
            if (ModelState.IsValid)
            {
                db.GameStarters.Add(gameStarter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gameStarter);
        }

        // GET: Gamer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameStarter gameStarter = await db.GameStarters.FindAsync(id);
            if (gameStarter == null)
            {
                return HttpNotFound();
            }
            return View(gameStarter);
        }

        // POST: Gamer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,begin")] GameStarter gameStarter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameStarter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gameStarter);
        }

        // GET: Gamer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameStarter gameStarter = await db.GameStarters.FindAsync(id);
            if (gameStarter == null)
            {
                return HttpNotFound();
            }
            return View(gameStarter);
        }

        // POST: Gamer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GameStarter gameStarter = await db.GameStarters.FindAsync(id);
            db.GameStarters.Remove(gameStarter);
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
