﻿using System;
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

namespace VinylVerse.Windows
{
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
            WindowDragHelper.EnableDrag(this, this);
        }
    }
}
