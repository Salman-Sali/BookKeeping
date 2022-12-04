using Bk.UserInterface.Views.Login;
using MediatR;
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

namespace Bk.UserInterface.Views.Layout
{
    /// <summary>
    /// Interaction logic for TopBarControl.xaml
    /// </summary>
    public partial class TopBarControl : UserControl
    {
        private readonly IMediator _mediator;
        private readonly MainWindow _mainWindow;

        public TopBarControl(IMediator mediator, MainWindow mainWindow)
        {
            InitializeComponent();

            _mediator = mediator;
            _mainWindow = mainWindow;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Logout();
        }

        public void ResetContents()
        {
            TitleText.Text = null;
            HomeButtonContentControl.Content = null;
            ButtonsContentControl.Content = null;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Settings();
        }
    }
}
