using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DsbForsinket.Net.FakeNotificationHub.Models;

namespace DsbForsinket.Net.FakeNotificationHub.Controllers
{
    public class RegistrationsController : Controller
    {
        private FakeNotificationHubContext db = new FakeNotificationHubContext();

        public async Task<ActionResult> Index()
        {
            var registrations = await db.Registrations.Include("Notifications").ToListAsync();
            return View(registrations);
        }

        public async Task<ActionResult> DeleteAll()
        {
            db.Registrations.RemoveRange(await db.Registrations.ToListAsync());
            db.Notifications.RemoveRange(await db.Notifications.ToListAsync());
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteAllNotifications()
        {
            db.Notifications.RemoveRange(await db.Notifications.ToListAsync());
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Registrations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Registration registration)
        {
            if (ModelState.IsValid)
            {
                registration.Station = registration.Station.Trim();
                db.Registrations.Add(registration);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Registration registration = await db.Registrations.FindAsync(id);
            db.Registrations.Remove(registration);
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
