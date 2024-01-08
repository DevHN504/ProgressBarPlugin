using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgressBarPlugin
{
    public partial class MainPage : ContentPage
    {
        private static int _progress;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ShowNotification(object sender, EventArgs e)
        {
            _progress = 0;

            var notificationRequest = new NotificationRequest
            {
                NotificationId = new Random().Next(),
                Title = "Progress Bar",
                Description = $"{_progress}%",
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions()
                {
                    ProgressBarMax = 100,
                    ProgressBarProgress = 0,
                    IsProgressBarIndeterminate = false
                }
            };

            await LocalNotificationCenter.Current.Show(notificationRequest);

            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                _progress += 10;

                notificationRequest.Android.ProgressBarProgress = _progress;
                notificationRequest.Description = $"{_progress}%";
                LocalNotificationCenter.Current.Show(notificationRequest);

                if (_progress == 100)
                {
                    return false;
                }

                return true;
            });
        }
    }
}
