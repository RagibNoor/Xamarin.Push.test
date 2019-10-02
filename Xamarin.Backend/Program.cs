using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Backend.Extensions;
using Xamarin.Backend.Models;

namespace Xamarin.Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = string.Empty;
            string body = string.Empty;
            string installId = string.Empty;
            char sendChoice = 'n';
            Console.WriteLine("Visual Studio App Center");

            Console.WriteLine("Please Enter Push Notification Title");
            title = Console.ReadLine();

            Console.WriteLine("Please Enter Push Notification body");
            body = Console.ReadLine();
            SendPushNotification(title, body).ContinueWith(async res =>
            {
                if (await res)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("error");
                }

                Console.ReadLine();
            });

        }

        private static async Task<bool> SendPushNotification(string title, string body)
        {
            string appName = "XF.push.droid";

            NotificationContent notificationContent = new NotificationContent()
            {
                Body = body,
                Title = title,
                Name = "test from console"

            };
            PushNotification pushNotification = new PushNotification()
            {
                NotificationContent = notificationContent
            };
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("X-API-Token", "eaa5ff1e8acad67c6a88103a4dfedbd5352182e5");
            var stringContent = pushNotification.AsJson();
            try
            {
                HttpResponseMessage httpResponseMessage =
                    await httpClient.PostAsync(
                        "https://appcenter.ms/api/v0.1/apps/" + "ragibnoor/XF.push.droid/push/notifications"
                        , stringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var json = await httpResponseMessage.Content.ReadAsStringAsync();
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }

            return false;

        }
    }
}
