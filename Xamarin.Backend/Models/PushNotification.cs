using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Backend.Models
{
     public class PushNotification
    {
        public NotificationTarget NotificationTarget { get; set; }
        public NotificationContent NotificationContent { get; set; }
    }
}
