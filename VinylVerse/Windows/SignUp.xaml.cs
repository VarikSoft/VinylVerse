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

        private int previousStrength = -1;

        private void textbox_password_PasswordTextChanged(object sender, RoutedEventArgs e)
        {
            string password = textbox_password.Password;
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
        }
    }
}
