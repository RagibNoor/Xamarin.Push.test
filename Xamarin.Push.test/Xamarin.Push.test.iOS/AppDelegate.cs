using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

using Foundation;
using UIKit;
using UserNotifications;

namespace Xamarin.Push.test.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private YourOwnUnUserNotificationCenterDelegate _myOwnNotificationDelegate = null;

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            #region  Handle push notification

            if (!AppCenter.Configured)
            {
                Microsoft.AppCenter.Push.Push.PushNotificationReceived += OnPushNotificationReceived;
            }
            #endregion
            #region start app service
            //AppCenter.Start("e8626b43-7fb9-4fbf-a9c1-127a07f3199e", typeof(Microsoft.AppCenter.Push.Push));
            #endregion
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                this._myOwnNotificationDelegate =new YourOwnUnUserNotificationCenterDelegate();
                UNUserNotificationCenter.Current.Delegate = this._myOwnNotificationDelegate;
            }
            return base.FinishedLaunching(app, options);
        }
        #region  Event for showing content

        private void OnPushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            // Add the notification message and title to the message
            var summary = $"Push notification received:" +
                          $"\n\tNotification title: {e.Title}" +
                          $"\n\tMessage: {e.Message}";

            // If there is custom data associated with the notification,
            // print the entries
            if (e.CustomData != null)
            {
                summary += "\n\tCustom data:\n";
                foreach (var key in e.CustomData.Keys)
                {
                    summary += $"\t\t{key} : {e.CustomData[key]}\n";
                }
            }
            if (this._myOwnNotificationDelegate.didReceiveNotificationInForeground)
            {
                
                
                // Present Alert
                //PresentViewController(okAlertController, true, null);
                UIAlertView _error = new UIAlertView("forground", "message recived in foreground", null, "Ok", null);

                _error.Show();
            }
            


            // Reset the property for next notifications.
            this._myOwnNotificationDelegate.didReceiveNotificationInForeground = false;
            // Send the notification summary to debug output
            System.Diagnostics.Debug.WriteLine(summary);
        }
        #endregion

        #region Disable automatic method forwarding to App Center services

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Microsoft.AppCenter.Push.Push.RegisteredForRemoteNotifications(deviceToken);
        }
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Microsoft.AppCenter.Push.Push.FailedToRegisterForRemoteNotifications(error);
        }
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            var result = Microsoft.AppCenter.Push.Push.DidReceiveRemoteNotification(userInfo);
            if (result)
            {
                completionHandler(UIBackgroundFetchResult.NewData);
            }
            else
            {
                completionHandler(UIBackgroundFetchResult.NoData);
            }
        }

        #endregion

    }
}
