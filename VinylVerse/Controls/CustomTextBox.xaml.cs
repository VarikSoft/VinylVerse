using System;
using System.Windows;
using System.Windows.Controls;

namespace VinylVerse.Controls
{
    public partial class CustomTextBox : UserControl
    {
        private string _lastText = string.Empty; // Храним предыдущее значение текста

        public CustomTextBox()
        {
            InitializeComponent();
            UpdatePlaceholderVisibility();
        }

        /// <summary>
        /// Событие, уведомляющее об изменении текста.
        /// </summary>
        public static readonly RoutedEvent TextChangedEvent =
            EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(CustomTextBox));

        /// <summary>
        /// CLR-обертка для события.
        /// </summary>
        public event RoutedEventHandler TextChanged
        {
            add => AddHandler(TextChangedEvent, value);
            remove => RemoveHandler(TextChangedEvent, value);
        }

        /// <summary>
        /// Dependency property для текста.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomTextBox),
                new PropertyMetadata(string.Empty, OnTextChanged));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextBox ct)
            {
                ct.UpdatePlaceholderVisibility();
            }
        }

        /// <summary>
        /// Dependency property для placeholder'а.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(CustomTextBox),
                new PropertyMetadata("Enter text here"));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private void PART_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void PART_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void PART_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Проверяем, изменился ли текст
            if (PART_TextBox.Text != _lastText)
            {
                _lastText = PART_TextBox.Text;
                Text = _lastText; // Обновляем свойство Text

                RaiseEvent(new RoutedEventArgs(TextChangedEvent)); // Вызываем событие TextChanged
            }

            UpdatePlaceholderVisibility();
        }

        /// <summary>
        /// Показывает placeholder, если текст пустой и поле не в фокусе.
        /// </summary>
        private void UpdatePlaceholderVisibility()
        {
            PlaceholderTextBlock.Visibility =
                string.IsNullOrEmpty(PART_TextBox.Text) && !PART_TextBox.IsFocused
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }
    }
}