using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DsbForsinket.Net.FakeNotificationHub.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public string Station { get; set; }
        public virtual List<Notification> Notifications { get; set; }
    }
}