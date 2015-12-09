﻿using DentalClinic.UI.Interfaces;
using System.Windows;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IClosable
    {
        public LoginWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
