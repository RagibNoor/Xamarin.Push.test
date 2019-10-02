using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xamarin.Backend.Extensions
{
    public static class ObjectExtensions
    {
        public static StringContent AsJson(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return  new StringContent(json , Encoding.UTF8 , "application/json");
        }
    }
}
