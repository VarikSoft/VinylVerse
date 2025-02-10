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

        /// <summary>
        /// Анимирует изменение цвета указанного элемента через HEX-цвета.
        /// </summary>
        /// <param name="element">Элемент WPF, для которого выполняется анимация.</param>
        /// <param name="propertyPath">Свойство элемента, к которому применяется анимация (например, Background или Foreground).</param>
        /// <param name="fromHexColor">Начальный HEX-цвет (например, "#D7D7D7").</param>
        /// <param name="toHexColor">Конечный HEX-цвет (например, "#55D78B").</param>
        /// <param name="durationMs">Длительность анимации в миллисекундах.</param>
        public static void AnimateColor(DependencyObject element, DependencyProperty propertyPath, string fromHexColor, string toHexColor, int durationMs = 300)
        {
            // Конвертация HEX-цветов в Color
            Color fromColor = (Color)ColorConverter.ConvertFromString(fromHexColor);
            Color toColor = (Color)ColorConverter.ConvertFromString(toHexColor);

            // Создаем анимацию цвета
            ColorAnimation colorAnimation = new ColorAnimation
            {
                From = fromColor,
                To = toColor,
                Duration = TimeSpan.FromMilliseconds(durationMs),
                EasingFunction = new QuadraticEase() // Плавное ускорение и замедление
            };

            // Создаем SolidColorBrush и применяем анимацию
            SolidColorBrush brush = new SolidColorBrush(fromColor);
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Устанавливаем анимированный SolidColorBrush на указанное свойство элемента
            if (element is FrameworkElement frameworkElement)
            {
                frameworkElement.SetValue(propertyPath, brush);
            }
        }

        /// <summary>
        /// Анимирует масштабирование элемента только вправо или влево, без смещения его позиции.
        /// </summary>
        /// <param name="element">Элемент, который нужно масштабировать.</param>
        /// <param name="scaleX">Конечное значение масштаба по оси X.</param>
        /// <param name="durationMs">Длительность анимации в миллисекундах.</param>
        public static void AnimateScale(UIElement element, double scaleX, int durationMs = 300)
        {
            // Устанавливаем ScaleTransform с начальным масштабом
            ScaleTransform scaleTransform = new ScaleTransform(1.0, 1.0);
            element.RenderTransform = scaleTransform;
            element.RenderTransformOrigin = new Point(0.0, 0.5); // Закрепляем левую сторону

            // Анимация масштабирования по оси X
            DoubleAnimation scaleXAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = scaleX,
                Duration = TimeSpan.FromMilliseconds(durationMs),
                EasingFunction = new QuadraticEase() // Плавное ускорение и замедление
            };

            // Запуск анимации
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        }
    }
}
