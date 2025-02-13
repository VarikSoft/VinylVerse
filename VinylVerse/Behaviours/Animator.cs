using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace VinylVerse.Behaviours
{
    internal class Animator
    {
        public static void TransitionWindows(Window oldWindow, Window newWindow, double durationSeconds)
        {
            newWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            newWindow.Top = oldWindow.Top;
            newWindow.Left = oldWindow.Left;

            FadeOut(oldWindow, durationSeconds, () =>
            {
                oldWindow.Hide();

                newWindow.Opacity = 0;
                newWindow.Show();

                FadeIn(newWindow, durationSeconds, () =>
                {
                    oldWindow.Close();
                });
            });
        }

        public static void FadeOut(UIElement element, double durationSeconds, Action completed = null)
        {
            var fadeOutAnimation = new DoubleAnimation
            {
                From = element.Opacity,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(durationSeconds))
            };

            if (completed != null)
                fadeOutAnimation.Completed += (s, e) => completed();

            element.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }

        public static void FadeIn(UIElement element, double durationSeconds, Action completed = null)
        {
            var fadeInAnimation = new DoubleAnimation
            {
                From = element.Opacity,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(durationSeconds))
            };

            if (completed != null)
                fadeInAnimation.Completed += (s, e) => completed();

            element.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }

        public static void AnimateColor(DependencyObject element, DependencyProperty propertyPath, string fromHexColor, string toHexColor, int durationMs = 300)
        {
            Color fromColor = (Color)ColorConverter.ConvertFromString(fromHexColor);
            Color toColor = (Color)ColorConverter.ConvertFromString(toHexColor);

            ColorAnimation colorAnimation = new ColorAnimation
            {
                From = fromColor,
                To = toColor,
                Duration = TimeSpan.FromMilliseconds(durationMs),
                EasingFunction = new QuadraticEase()
            };

            SolidColorBrush brush = new SolidColorBrush(fromColor);
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            if (element is FrameworkElement frameworkElement)
            {
                frameworkElement.SetValue(propertyPath, brush);
            }
        }

        public static void AnimateScale(UIElement element, double scaleX, int durationMs = 300)
        {
            ScaleTransform scaleTransform = new ScaleTransform(1.0, 1.0);
            element.RenderTransform = scaleTransform;
            element.RenderTransformOrigin = new Point(0.0, 0.5);

            DoubleAnimation scaleXAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = scaleX,
                Duration = TimeSpan.FromMilliseconds(durationMs),
                EasingFunction = new QuadraticEase()
            };

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        }

        public static void ToggleVisibility(UIElement element, double durationSeconds)
        {
            if (element.Visibility == Visibility.Visible)
            {
                FadeOut(element, durationSeconds, () =>
                {
                    element.Visibility = Visibility.Collapsed;
                });
            }
            else
            {
                element.Visibility = Visibility.Visible;
                element.Opacity = 0;
                FadeIn(element, durationSeconds);
            }
        }
    }
}