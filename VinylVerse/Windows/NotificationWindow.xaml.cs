using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VinylVerse.Windows
{
    /// <summary>
    /// Логика взаимодействия для NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private static readonly List<NotificationWindow> OpenNotifications = new List<NotificationWindow>();

        public NotificationWindow(string message)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;
            Loaded += NotificationWindow_Loaded;
        }

        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var workingArea = SystemParameters.WorkArea;
            double margin = 10; 

            double bottomOffset = margin + 60;
            foreach (var win in OpenNotifications)
            {
                bottomOffset += win.Height + margin;
            }
            Left = workingArea.Right - Width - margin;
            Top = workingArea.Bottom - bottomOffset;

            OpenNotifications.Add(this);

            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.2)));
            BeginAnimation(Window.OpacityProperty, fadeIn);

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.2)));
                fadeOut.Completed += (s2, args2) => Close();
                BeginAnimation(Window.OpacityProperty, fadeOut);
            };
            timer.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            OpenNotifications.Remove(this);
            base.OnClosed(e);
            RearrangeNotifications();
        }
        private void RearrangeNotifications()
        {
            var workingArea = SystemParameters.WorkArea;
            double margin = 10;
            double bottomOffset = margin;

            foreach (var win in OpenNotifications)
            {
                double targetTop = workingArea.Bottom - bottomOffset - win.Height;

                var animation = new DoubleAnimation(win.Top, targetTop, new Duration(TimeSpan.FromSeconds(0.2)))
                {
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };
                win.BeginAnimation(Window.TopProperty, animation);

                bottomOffset += win.Height + margin;
            }
        }
        public static void ShowNotification(string message)
        {
            var notification = new NotificationWindow(message);
            notification.Show();
        }
    }
}