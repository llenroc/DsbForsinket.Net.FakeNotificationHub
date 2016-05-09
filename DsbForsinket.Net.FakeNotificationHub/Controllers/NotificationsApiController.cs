using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DsbForsinket.Net.FakeNotificationHub.Models;
using DsbForsinket.Net.FakeNotificationHub.ViewModel;

namespace DsbForsinket.Net.FakeNotificationHub.Controllers
{
    public class NotificationsApiController : ApiController
    {
        private FakeNotificationHubContext db = new FakeNotificationHubContext();

        // POST: api/Notifications
        [ResponseType(typeof(Notification))]
        public async Task<string> PostNotification(NotificationViewModel notification)
        {
            // make the call slower
            await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(5000)));

            var time = notification.Tag.Split('-')[1];
            var station = notification.Tag.Split('-')[0];

            var registrations = await db.Registrations.Where(r => r.Station == station).ToListAsync();
            foreach(var r in registrations)
            {
                r.Notifications.Add(new Notification
                {
                    RegistrationId = r.Id,
                    Timestamp = DateTime.UtcNow,
                    Content = notification.Body,
                    Tag = notification.Tag
                });
            }

            await db.SaveChangesAsync();

            return "OK";
        }
    }
}