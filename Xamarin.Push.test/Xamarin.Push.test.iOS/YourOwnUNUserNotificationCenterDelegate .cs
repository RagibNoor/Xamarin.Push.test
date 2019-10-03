using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserNotifications;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

using Foundation;
using UIKit;

namespace Xamarin.Push.test.iOS
{
    public class YourOwnUnUserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        // This is a property that it is exposed so it can be used elsewhere.
        public bool didReceiveNotificationInForeground { get; set; }


        #region Handle a push notification while the app is in foreground

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            // Do something, e.g. set a Boolean property to track the foreground state.
            this.didReceiveNotificationInForeground = true;

            // This callback overrides the system default behavior, so MSPush callback should be proxied manually.
            Microsoft.AppCenter.Push.Push.DidReceiveRemoteNotification(notification.Request.Content.UserInfo);

            // Complete handling the notification.
            completionHandler(UNNotificationPresentationOptions.None);
        }

        #endregion

        #region Detecting when a user has tapped on a push notification

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            if (response.IsDefaultAction)
            {
                // User tapped on notification
                UIAlertView _error = new UIAlertView("forground", "Data sync needed", null, "Ok", null);

                _error.Show();
            }

            // This callback overrides the system default behavior, so MSPush callback should be proxied manually.
            Microsoft.AppCenter.Push.Push.DidReceiveRemoteNotification(response.Notification.Request.Content.UserInfo);

            // Complete handling the notification.
            completionHandler();
        }

        #endregion
    }
}