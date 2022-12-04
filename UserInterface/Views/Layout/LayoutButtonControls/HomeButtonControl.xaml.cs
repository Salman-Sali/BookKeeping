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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bk.UserInterface.Views.Layout
{
    /// <summary>
    /// Interaction logic for HomeButtonControl.xaml
    /// </summary>
    public partial class HomeButtonControl : UserControl
    {
        private readonly MainWindow _mainWindow;
        public HomeButtonControl(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Home();
        }
    }
}
