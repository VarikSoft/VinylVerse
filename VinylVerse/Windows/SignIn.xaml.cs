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
    }
}