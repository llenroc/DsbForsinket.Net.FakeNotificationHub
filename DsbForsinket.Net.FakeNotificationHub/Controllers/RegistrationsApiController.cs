using DsbForsinket.Net.FakeNotificationHub.Models;
using DsbForsinket.Net.FakeNotificationHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DsbForsinket.Net.FakeNotificationHub.Controllers
{
    public class RegistrationsApiController : ApiController
    {
        private FakeNotificationHubContext db = new FakeNotificationHubContext();

        public async Task<List<RegistrationViewModel>> GetRegistrations(string tag)
        {
            int bucket = int.Parse(tag.Substring(tag.Length - 1));

            var cphTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Romance Standard Time");
            var minutesRounded = (cphTime.Minute / 15) * 15;
            var timeTag = $"time-{cphTime.Hour:D2}:{minutesRounded:D2}";
            var timeTagNoPrefix = $"{cphTime.Hour:D2}:{minutesRounded:D2}";

            var registrations = await db.Registrations.ToListAsync();
            var registratonsInBucket = registrations.Where(r => r.Id % 10 == bucket);
            return registratonsInBucket.Select(r => new RegistrationViewModel
            {
                Tags = new List<string> { $"station-{r.Station}", timeTag, $"{r.Station}-{timeTagNoPrefix}" }
            }).ToList();
        }
    }
}
