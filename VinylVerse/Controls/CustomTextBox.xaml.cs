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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinylVerse.Controls
{
    public partial class CustomTextBox : UserControl
    {
        public CustomTextBox()
        {
            InitializeComponent();
            UpdatePlaceholderVisibility();
        }

        /// <summary>
        /// Dependency property to bind the text entered by the user.
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
        /// Dependency property to set the placeholder text.
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
            Text = PART_TextBox.Text;
            UpdatePlaceholderVisibility();
        }

        /// <summary>
        /// Shows the placeholder when there is no text and the TextBox is not focused.
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
