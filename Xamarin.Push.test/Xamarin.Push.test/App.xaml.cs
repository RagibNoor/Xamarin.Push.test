using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Xamarin.Push.test.utils;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xamarin.Push.test
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            #region  Handle push notification

            if (!AppCenter.Configured)
            {
               // Microsoft.AppCenter.Push.Push.PushNotificationReceived += OnPushNotificationReceived;
            }
            #endregion
            #region start app service
            
            AppCenter.Start("7527047f-67e1-48d5-9e4d-a0bc4294fdf8", typeof(Microsoft.AppCenter.Push.Push));
            #endregion

            #region save app center install Id

            AppCenter.GetInstallIdAsync().ContinueWith(installid =>
            {
                System.Diagnostics.Debug.WriteLine("********************");
                System.Diagnostics.Debug.WriteLine("app center id = " + installid.Result);
                System.Diagnostics.Debug.WriteLine("********************");
            });
            #endregion
        }


        #region  Event for showing content

        private void OnPushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    Current.MainPage.DisplayAlert(e.Title, e.Message, "ok");
                });
        }
        #endregion

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
