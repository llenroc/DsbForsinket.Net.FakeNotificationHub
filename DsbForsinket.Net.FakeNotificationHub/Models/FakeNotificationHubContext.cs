using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DsbForsinket.Net.FakeNotificationHub.Models
{
    public class FakeNotificationHubContext : DbContext
    {
        public FakeNotificationHubContext() : base("name=FakeNotificationHubContext")
        {
            Database.SetInitializer<FakeNotificationHubContext>(new DropCreateDatabaseIfModelChanges<FakeNotificationHubContext>());
        }

        public System.Data.Entity.DbSet<DsbForsinket.Net.FakeNotificationHub.Models.Registration> Registrations { get; set; }
        public System.Data.Entity.DbSet<DsbForsinket.Net.FakeNotificationHub.Models.Notification> Notifications { get; set; }
    }
}
