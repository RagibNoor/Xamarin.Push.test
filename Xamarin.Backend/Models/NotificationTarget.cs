using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Backend.Models
{
    public class NotificationTarget
    {
        public string Type { get; set; }
        public List<string> Devices { get; set; }
    }
}
