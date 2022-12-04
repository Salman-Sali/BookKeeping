using Bk.Infrastructure.Queries.Users;
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
    /// Interaction logic for UserDetailsView.xaml
    /// </summary>
    public partial class UserDetailsView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly IMediator _mediator;
        private readonly UserResponse _user;
        public UserDetailsView(IMediator mediator, MainWindow mainWindow, UserResponse user)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mediator = mediator;
            _user = user;
            LoadUser();
        }

        private void LoadUser()
        {
            IdText.Text = _user.Id.ToString();
            UserNameText.Text = _user.UserName;
            PasswordText.Text = _user.Password;
            CreatedByText.Text = _user.CreatedBy + " On " + _user.CreatedOn;
            UpdatedByText.Text = _user.UpdatedBy + " On " + _user.UpdatedOn;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await _mediator.Send(new UpdateUserCommand
            {
                UserName = UserNameText.Text,
                Password = PasswordText.Text,
                UserId = _user.Id,
                UpdaterUserId = LoginView.UserId.Value
            });
            _mainWindow.UserList();
            MessageBox.Show("User Updated.");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.UserList();
        }
    }
}
