using BK.Application.Commands.Books;
using Bk.Infrastructure.Queries.Books;
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

namespace Bk.UserInterface.Views.Book
{
    /// <summary>
    /// Interaction logic for FDCBookDetailsView.xaml
    /// </summary>
    public partial class FDCBookDetailsView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly IMediator _mediator;
        private readonly int BookId;
        public FDCBookDetailsView(MainWindow mainWindow, IMediator mediator, int bookId)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mediator = mediator;
            BookId = bookId;
            LoadBookDetails();
        }

        public async void LoadBookDetails()
        {
            var result = await _mediator.Send(new GetBookByIdQuery { BookId = BookId });
            NameTextBox.Text = result.Name;
            PhoneTextBox.Text = result.Phone;
            DiscountPerLitreTextBox.Text = result.DiscountPerLitre.ToString();
            BookTypeText.Text = result.BookType;
            CreatedByText.Text = result.CreatedBy + " On " + result.CreatedOn;
            UpdatedByText.Text = result.UpdatedBy + " On " + result.UpdatedOn;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if(!float.TryParse(DiscountPerLitreTextBox.Text, out float discount))
            {
                throw new Exception("Discount per litre should be a number.");
            }
            await _mediator.Send(new UpdateBookCommand
            {
                BookId = BookId,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                UserId = LoginView.UserId.Value,
                DiscountPerLitre = discount
            });
            _mainWindow.ShowBookEntries(BookId);
            MessageBox.Show("Book Details Updated!");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowBookEntries(BookId);
        }
    }
}
