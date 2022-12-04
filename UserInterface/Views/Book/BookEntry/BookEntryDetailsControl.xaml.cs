using Bk.Infrastructure.Queries.BookEntries;
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
    /// Interaction logic for BookEntryDetailsControl.xaml
    /// </summary>
    public partial class BookEntryDetailsControl : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly IMediator _mediator;
        private readonly BookEntryResponse _entry;
        public BookEntryDetailsControl(IMediator mediator, BookEntryResponse entry, MainWindow mainWindow)
        {
            InitializeComponent();
            _mediator = mediator;
            _mainWindow = mainWindow;
            _entry = entry;
            LoadEntry();
        }

        private void LoadEntry()
        {
            IdText.Text = _entry.Id.ToString();
            SerialText.Text = _entry.SerialNumber.ToString();
            DescriptionText.Text = _entry.Description;
            DateDatePicker.SelectedDate = DateTime.Parse(_entry.Date);
            DebitText.Text = _entry.Debit.ToString();
            CreditText.Text = _entry.Credit.ToString();
            CreatedByText.Text = _entry.CreatedBy + " On " + _entry.CreatedOn;
            UpdatedByText.Text = _entry.UpdatedBy + " On " + _entry.UpdatedOn;

            if (string.IsNullOrEmpty(CreditText.Text) && !string.IsNullOrEmpty(DebitText.Text))
            {
                CreditText.Visibility = Visibility.Hidden;
                DebitText.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(DebitText.Text) && !string.IsNullOrEmpty(CreditText.Text))
            {
                DebitText.Visibility = Visibility.Hidden;
                CreditText.Visibility = Visibility.Visible;
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            float? debit = null;
            float? credit = null;

            var debitConverted = float.TryParse(DebitText.Text, out float outDebit);
            if (!string.IsNullOrEmpty(DebitText.Text) && !debitConverted)
            {
                throw new Exception("Debit must be a number.");
            }
            else if (!string.IsNullOrEmpty(DebitText.Text))
            {
                debit = outDebit;
            }

            var creditConverted = float.TryParse(CreditText.Text, out float outCredit);
            if (!string.IsNullOrEmpty(CreditText.Text) && !creditConverted)
            {
                throw new Exception("Credit must be a number.");
            }
            else if (!string.IsNullOrEmpty(CreditText.Text))
            {
                credit = outCredit;
            }

            await _mediator.Send(new UpdateBookEntryCommand
            {
                BookEntryId = _entry.Id,
                Description = DescriptionText.Text,
                Date = DateDatePicker.SelectedDate.ToString(),
                Debit = debit,
                Credit = credit,
                UserId = LoginView.UserId.Value
            });
            _mainWindow.ShowBookEntries(_entry.BookId);
            MessageBox.Show("Entry updated!");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowBookEntries(_entry.BookId);
        }

        private void CreditText_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(CreditText.Text))
            {
                DebitText.Visibility = Visibility.Visible;
            }
            else
            {
                DebitText.Visibility = Visibility.Hidden;
            }

        }

        private void DebitText_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(DebitText.Text))

            {
                CreditText.Visibility = Visibility.Visible;
            }
            else
            {
                CreditText.Visibility = Visibility.Hidden;
            }
        }
    }
}
