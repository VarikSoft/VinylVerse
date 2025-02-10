using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace VinylVerse.Controls
{
    public partial class StatusLabel : UserControl
    {
        public StatusLabel()
        {
            InitializeComponent();
            // Вызываем обновление состояния после загрузки контрола
            this.Loaded += StatusLabel_Loaded;
        }

        private void StatusLabel_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateVisualState();
        }

        // DependencyProperty для IsChecked
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(StatusLabel),
                new PropertyMetadata(false, OnIsCheckedChanged));

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is StatusLabel label)
            {
                label.UpdateVisualState();
            }
        }

        // DependencyProperty для текста метки
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(StatusLabel),
                new PropertyMetadata("Label text"));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Метод обновления состояния, который напрямую изменяет свойства
        public void UpdateVisualState()
        {
            if (IsChecked)
            {
                AnimateOpacity(DotIcon, 1, 0); // Скрываем точку
                AnimateOpacity(CheckIcon, 0, 1); // Показываем галочку
                AnimateColor(LabelText, "#D7D7D7", "#55D78B"); // Зеленый цвет текста
            }
            else
            {
                AnimateOpacity(DotIcon, 0, 1); // Показываем точку
                AnimateOpacity(CheckIcon, 1, 0); // Скрываем галочку
                AnimateColor(LabelText, "#55D78B", "#D7D7D7"); // Серый цвет текста
            }
        }

        private void AnimateOpacity(UIElement element, double from, double to)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase() // Плавное ускорение и замедление
            };
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        private void AnimateColor(TextBlock textBlock, string fromColor, string toColor)
        {
            ColorAnimation animation = new ColorAnimation
            {
                From = (Color)ColorConverter.ConvertFromString(fromColor),
                To = (Color)ColorConverter.ConvertFromString(toColor),
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase() // Плавное изменение цвета
            };

            textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fromColor));
            textBlock.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        // Метод для переключения состояния
        public void ToggleState(bool newState)
        {
            IsChecked = newState;
            // Метод OnIsCheckedChanged вызовет UpdateVisualState автоматически
        }
    }
}
