using Bk.Domain.Enums;
using Bk.Infrastructure.Queries.Books;
using Bk.UserInterface.Views.Login;
using BK.Application.Commands.Books;
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

namespace Bk.UserInterface.Views.Book.CreateBook
{
    /// <summary>
    /// Interaction logic for CreateBookView.xaml
    /// </summary>
    public partial class CreateBookView : UserControl
    {
        private readonly IMediator _mediator;
        private readonly MainWindow _mainWindow;
        public CreateBookView(IMediator mediator, MainWindow mainWindow)
        {
            InitializeComponent();
            _mediator = mediator;
            _mainWindow = mainWindow;
            BookTypesAsync();
        }

        public async void BookTypesAsync()
        {
            var result = await _mediator.Send(new ListBookTypesQuery());
            foreach (var item in result.BookTypes)
            {
                BookTypeComboBox.Items.Add(item);
            }
        }

        private void BookTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            DiscountPerLitreTextBox.Text = null;
            if (BookTypeComboBox.Text == BookType.FuelCreditDiscountBook.ToString())
            {
                DiscountPerLitreGrid.Visibility = Visibility.Visible;
            }
            else
            {
                DiscountPerLitreGrid.Visibility = Visibility.Hidden;
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            await _mediator.Send(new AddBookCommand
            {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                BookType = BookTypeComboBox.Text,
                DiscountPerLitre = float.TryParse(DiscountPerLitreTextBox.Text, out var result) ? result : null,
                UserId = LoginView.UserId.Value
            });
            _mainWindow.Home();
            MessageBox.Show("Book successfully created.", "Success!", MessageBoxButton.OK, MessageBoxImage.None);
        }
    }
}
