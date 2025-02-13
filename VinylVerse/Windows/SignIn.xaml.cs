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
using System.Windows.Shapes;

using VinylVerse.Behaviours;
using VinylVerse.Controls;

namespace VinylVerse.Windows
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
            WindowDragHelper.EnableDrag(this, this);
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            var signUpWindow = new SignUp();
            Animator.TransitionWindows(this, signUpWindow, 0.2);
        }

        private void UsernameTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            bool shouldBeVisible = username_textbox.Text.Length > 0;

            if (shouldBeVisible && username_check.Visibility != Visibility.Visible)
            {
                Animator.ToggleVisibility(username_check, 0.2);
                Animator.ToggleVisibility(username_circle, 0.2);
                Animator.AnimateColor(usernameLine, Border.BackgroundProperty, "#D7D7D7", "#55D78B", 200);
            }
            else if (!shouldBeVisible && username_check.Visibility == Visibility.Visible)
            {
                Animator.ToggleVisibility(username_check, 0.2);
                Animator.ToggleVisibility(username_circle, 0.2);
                Animator.AnimateColor(usernameLine, Border.BackgroundProperty, "#55D78B", "#D7D7D7", 200);
            }
        }

        private string lastAppliedColor = "#D7D7D7";

        private void textbox_password_PasswordTextChanged(object sender, RoutedEventArgs e)
        {
            string newColor;

            if (signIn_password_textbox.Password.Length > 0)
            {
                newColor = "#55D78B";
            }
            else
            {
                newColor = "#D7D7D7";
            }

            if (lastAppliedColor != newColor)
            {
                Animator.AnimateColor(passwordLine, Border.BackgroundProperty, lastAppliedColor, newColor, 200);
                lastAppliedColor = newColor;
            }
        }
    }
}