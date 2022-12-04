using Bk.UserInterface.Views.Login;
using BK.Application.Commands.Users;
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

namespace Bk.UserInterface.Views.Settings.Users
{
    /// <summary>
    /// Interaction logic for CreateUserView.xaml
    /// </summary>
    public partial class CreateUserView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly IMediator _mediator;
        public CreateUserView(IMediator mediator, MainWindow mainWindow)
        {
            InitializeComponent();
            _mediator = mediator;
            _mainWindow = mainWindow;
        }

        private async void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            await _mediator.Send(new AddUserCommand
            {
                UserName = UserNameText.Text,
                Password = PasswordText.Text,
                UserId = LoginView.UserId.Value
            });
            _mainWindow.UserList();
            MessageBox.Show("User added successfully.");
        }
    }
}
