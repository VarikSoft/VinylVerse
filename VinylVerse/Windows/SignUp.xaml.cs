using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

using VinylVerse.Behaviours;
using VinylVerse.Controls;

namespace VinylVerse.Windows
{
    public partial class SignUp : Window
    {
        public int userPasswordStrength = 0;
        public string password;
        public SignUp()
        {
            InitializeComponent();
            WindowDragHelper.EnableDrag(this, this);
        }

        public enum ConditionType
        {
            isHasEightSymbols,
            isHasOneNumberOrSymbol,
            isHasOneLowerCaseAndOneUpperCaseLetter
        }

        public bool isConditionCompleted(string password, ConditionType condition)
        {
            switch (condition)
            {
                case ConditionType.isHasEightSymbols:
                    if (password.Length >= 8)
                    {
                        return true;
                    }
                    break;
                case ConditionType.isHasOneNumberOrSymbol:
                    if(password.Any(char.IsDigit) || password.Any(char.IsSymbol))
                    {
                        return true;
                    }
                    break;
                case ConditionType.isHasOneLowerCaseAndOneUpperCaseLetter:
                    if(password.Any(char.IsLower) && password.Any(char.IsUpper))
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }

            return false;
        }

        public void passwordStrenthCheck(int strength)
        {
            double targetScale;
            string targetColor;

            switch (strength)
            {
                case 1:
                    targetScale = 0.2;
                    targetColor = "#e74c3c"; // Красный
                    break;
                case 2:
                    targetScale = 0.5;
                    targetColor = "#f1c40f"; // Желтый
                    break;
                case 3:
                    targetScale = 0.67;
                    targetColor = "#55D78B"; // Зеленый
                    break;
                default:
                    targetScale = 0.0;
                    targetColor = "#D7D7D7"; // Серый (по умолчанию)
                    break;
            }

            ScaleTransform currentTransform = passwordStrengthLine.RenderTransform as ScaleTransform;
            if (currentTransform == null)
            {
                currentTransform = new ScaleTransform(1.0, 1.0);
                passwordStrengthLine.RenderTransform = currentTransform;
            }

            SolidColorBrush currentBrush = passwordStrengthLine.Background as SolidColorBrush;
            string currentColor = currentBrush != null ? currentBrush.Color.ToString() : "#D7D7D7";

            if (Math.Abs(currentTransform.ScaleX - targetScale) > 0.01)
            {
                Animator.AnimateScale(passwordStrengthLine, targetScale * 200, 500);
            }

            if (currentColor != targetColor)
            {
                Animator.AnimateColor(passwordStrengthLine, Border.BackgroundProperty, currentColor, targetColor, 500);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var signInWindow = new SignIn();
            Animator.TransitionWindows(this, signInWindow, 0.2);
        }

        private void UsernameTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            bool shouldBeVisible = username_textbox.Text.Length > 0;

            if (shouldBeVisible && username_check.Visibility != Visibility.Visible)
            {
                Animator.ToggleVisibility(username_check, 0.2);
                Animator.ToggleVisibility(username_circle, 0.2);
                Animator.AnimateColor(username_line, Border.BackgroundProperty, "#D7D7D7", "#55D78B", 200);
            }
            else if (!shouldBeVisible && username_check.Visibility == Visibility.Visible)
            {
                Animator.ToggleVisibility(username_check, 0.2);
                Animator.ToggleVisibility(username_circle, 0.2);
                Animator.AnimateColor(username_line, Border.BackgroundProperty, "#55D78B", "#D7D7D7", 200);
            }
        }

        private void EmailTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            bool shouldBeVisible = email_textbox.Text.Length > 0;

            if (shouldBeVisible && email_check.Visibility != Visibility.Visible)
            {
                Animator.ToggleVisibility(email_check, 0.2);
                Animator.ToggleVisibility(email_circle, 0.2);
                Animator.AnimateColor(email_line, Border.BackgroundProperty, "#D7D7D7", "#55D78B", 200);
            }
            else if (!shouldBeVisible && email_check.Visibility == Visibility.Visible)
            {
                Animator.ToggleVisibility(email_check, 0.2);
                Animator.ToggleVisibility(email_circle, 0.2);
                Animator.AnimateColor(email_line, Border.BackgroundProperty, "#55D78B", "#D7D7D7", 200);
            }
        }

        private string lastAppliedColor = "#D7D7D7";

        private void re_textbox_password_PasswordTextChanged(object sender, RoutedEventArgs e)
        {
            string newColor;

            if (textbox_re_password.Password == password && textbox_re_password.Password.Length > 0)
            {
                newColor = "#55D78B";
            }
            else if (textbox_re_password.Password.Length == 0)
            {
                newColor = "#D7D7D7";
            }
            else
            {
                newColor = "#e74c3c";
            }

            if (lastAppliedColor != newColor)
            {
                Animator.AnimateColor(rePasswordLine, Border.BackgroundProperty, lastAppliedColor, newColor, 200);
                lastAppliedColor = newColor;
            }
        }

        private int previousStrength = -1;

        private void textbox_password_PasswordTextChanged(object sender, RoutedEventArgs e)
        {
            password = textbox_password.Password;
            int userPasswordStrength = 0;

            if (isConditionCompleted(password, ConditionType.isHasEightSymbols))
            {
                Con1.ToggleState(true);
                userPasswordStrength++;
            }
            else
            {
                Con1.ToggleState(false);
            }

            if (isConditionCompleted(password, ConditionType.isHasOneNumberOrSymbol))
            {
                Con2.ToggleState(true);
                userPasswordStrength++;
            }
            else
            {
                Con2.ToggleState(false);
            }

            if (isConditionCompleted(password, ConditionType.isHasOneLowerCaseAndOneUpperCaseLetter))
            {
                Con3.ToggleState(true);
                userPasswordStrength++;
            }
            else
            {
                Con3.ToggleState(false);
            }

            if (userPasswordStrength != previousStrength)
            {
                previousStrength = userPasswordStrength;
                passwordStrenthCheck(userPasswordStrength);
            }

            if (textbox_re_password.Password.Length > 0) {
                string newColor;

                if (textbox_password.Password.Length == 0)
                {
                    newColor = "#D7D7D7";
                }
                else if (textbox_password.Password == textbox_re_password.Password)
                {
                    newColor = "#55D78B";
                }
                else
                {
                    newColor = "#e74c3c";
                }

                if (lastAppliedColor != newColor)
                {
                    Animator.AnimateColor(rePasswordLine, Border.BackgroundProperty, lastAppliedColor, newColor, 200);
                    lastAppliedColor = newColor;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NotificationWindow.ShowNotification("Test1");
        }
    }
}
