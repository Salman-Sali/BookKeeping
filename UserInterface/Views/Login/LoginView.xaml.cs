using Bk.Infrastructure.Queries.Users;
using Bk.UserInterface.Views.Home;
using Bk.UserInterface.Views.Layout;
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

namespace Bk.UserInterface.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private readonly IMediator _mediator;
        private readonly MainWindow _mainWindow;

        private static int? userId = null;
        public static int? UserId {
            get 
            {
                return userId;
            }
            private set 
            {
                userId = value;
            } 
        }

        private static string? userName = null;
        public static string? UserName
        {
            get
            {
                return userName;
            }
            private set
            {
                userName = value;
            }
        }

        public LoginView(IMediator mediator, MainWindow mainWindow)
        {
            InitializeComponent();

            _mediator = mediator;
            _mainWindow = mainWindow;

            userId = null;
            userName = null;
            var response = mediator.Send(new GetUserNamesQuery()).Result;
            foreach (var item in response.UserNames)
            {
                UserComboBox.Items.Add(item);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await LoginAsync();
        }

        public async Task LoginAsync()
        {
            userId = await _mediator.Send(new LoginUser { UserName = UserComboBox.Text, Password = UserPasswordBox.Password });
            if (userId == null)
            {
                throw new Exception("Invalid login.");
            }
            userName = UserComboBox.Text;
            _mainWindow.AfterLogin(userName);            
        }

        public void Logout()
        {
            userId = null;
            userName = null;
            UserPasswordBox.Password = null;
        }

        private async void UserPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                await LoginAsync();
            }
        }
    }
}
