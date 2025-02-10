using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                DotIcon.Opacity = 0;
                CheckIcon.Opacity = 1;
                LabelText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#55D78B"));
            }
            else
            {
                DotIcon.Opacity = 1;
                CheckIcon.Opacity = 0;
                LabelText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D7D7D7"));
            }
        }

        // Метод для переключения состояния
        public void ToggleState(bool newState)
        {
            IsChecked = newState;
            // Метод OnIsCheckedChanged вызовет UpdateVisualState автоматически
        }
    }
}
