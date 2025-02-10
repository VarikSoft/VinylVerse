using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace VinylVerse.Behaviours
{
    internal class Animator
    {
        /// <summary>
        /// Плавно скрывает старое окно, затем открывает новое окно в той же позиции с плавным появлением.
        /// После завершения анимации старое окно закрывается.
        /// </summary>
        /// <param name="oldWindow">Окно, которое нужно закрыть.</param>
        /// <param name="newWindow">Окно, которое нужно открыть.</param>
        /// <param name="durationSeconds">Длительность анимации (в секундах) для каждого этапа.</param>
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

        /// <summary>
        /// Анимирует затухание элемента (снижение Opacity до 0).
        /// </summary>
        /// <param name="element">UIElement для анимации (в данном случае окно).</param>
        /// <param name="durationSeconds">Длительность анимации в секундах.</param>
        /// <param name="completed">Опциональный callback по завершении анимации.</param>
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

        /// <summary>
        /// Анимирует появление элемента (увеличение Opacity до 1).
        /// </summary>
        /// <param name="element">UIElement для анимации (в данном случае окно).</param>
        /// <param name="durationSeconds">Длительность анимации в секундах.</param>
        /// <param name="completed">Опциональный callback по завершении анимации.</param>
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
    }
}
