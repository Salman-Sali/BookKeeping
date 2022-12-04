using Bk.UserInterface.Views.Login;
using BK.Application.Commands.BookEntries;
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

namespace Bk.UserInterface.Views.Book.BookEntry
{
    /// <summary>
    /// Interaction logic for CreateBookEntryView.xaml
    /// </summary>
    public partial class CreateBookEntryView : UserControl
    {
        private readonly IMediator _mediator;
        private readonly MainWindow _mainWindow;
        private readonly int BookId;
        public CreateBookEntryView(int bookId, MainWindow mainWindow, IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            _mainWindow = mainWindow;
            BookId = bookId;
            DateDatePicker.SelectedDate = DateTime.Now;
        }

        private void DebitTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(DebitTextBox.Text))

            {
                CreditGrid.Visibility = Visibility.Visible;
            }
            else
            {
                CreditGrid.Visibility = Visibility.Hidden;
            }
        }

        private void CreditTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(CreditTextBox.Text))
            {
                DebitGrid.Visibility = Visibility.Visible;
            }
            else
            {
                DebitGrid.Visibility = Visibility.Hidden;
            }
        }

        private async void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            float? debit = null;
            float? credit = null;

            var debitConverted = float.TryParse(DebitTextBox.Text, out float outDebit);
            if (!string.IsNullOrEmpty(DebitTextBox.Text) && !debitConverted)
            {
                throw new Exception("Debit must be a number.");
            }
            else if(!string.IsNullOrEmpty(DebitTextBox.Text))
            {
                debit = outDebit;
            }

            var creditConverted = float.TryParse(CreditTextBox.Text, out float outCredit);
            if (!string.IsNullOrEmpty(CreditTextBox.Text) && !creditConverted)
            {
                throw new Exception("Credit must be a number.");
            }
            else if (!string.IsNullOrEmpty(CreditTextBox.Text))
            {
                credit = outCredit;
            }

            await _mediator.Send(new AddBookEntryCommand
            {
                BookId = BookId,
                Date = DateDatePicker.SelectedDate.ToString(),
                Description = DescriptionTextBox.Text,
                Debit = debit,
                Credit = credit,
                UserId = LoginView.UserId.Value
            });
            _mainWindow.ShowBookEntries(BookId);
            MessageBox.Show("Inserted!");
        }
    }
}
