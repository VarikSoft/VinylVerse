﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
            WindowDragHelper.EnableDrag(this, this);
        }

        private void CustomTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            Con1.ToggleState(true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var signInWindow = new SignIn();
            Animator.TransitionWindows(this, signInWindow, 0.2);
        }
    }
}
