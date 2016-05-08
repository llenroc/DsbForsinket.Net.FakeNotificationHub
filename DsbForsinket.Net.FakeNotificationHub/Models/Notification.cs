using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DsbForsinket.Net.FakeNotificationHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}