using Bk.Infrastructure.Queries.Users;
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
    public partial class Login : UserControl
    {
        private readonly IMediator _mediator;
        public Login(IMediator mediator)
        {
            InitializeComponent();

            _mediator = mediator;

            var response = mediator.Send(new GetUserNamesQuery()).Result;
            foreach (var item in response.UserNames)
            {
                UserComboBox.Items.Add(item);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userId = await _mediator.Send(new LoginUser {  UserName = UserComboBox.Text, Password = UserPasswordBox.Password });
            if(userId == null)
            {
                throw new Exception("Invalid login.");
            }
        }
    }
}
