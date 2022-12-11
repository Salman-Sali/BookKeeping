using Bk.Infrastructure.Queries.BookEntries;
using BK.Application.Commands.BookEntries;
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
using Bk.Domain.Enums;
using Bk.Infrastructure.Queries.Books;

namespace Bk.UserInterface.Views.Book.BookEntry
{
    /// <summary>
    /// Interaction logic for FDCBookEntryDetailsView.xaml
    /// </summary>
    public partial class FDCBookEntryDetailsView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly IMediator _mediator;
        private readonly BookEntryResponse _entry;
        private float DiscountPerLitre;
        private bool OldDriver;
        private bool OldVehicle;
        public FDCBookEntryDetailsView(IMediator mediator, BookEntryResponse entry, MainWindow mainWindow)
        {
            InitializeComponent();
            _mediator = mediator;
            _mainWindow = mainWindow;
            _entry = entry;
            OldDriver = true;
            OldVehicle = true;
            LoadComboBoxesAsync();
            LoadEntry();
            LoadBookDetails();
        }

        private async void LoadBookDetails()
        {
            var bookDetails = await _mediator.Send(new GetBookByIdQuery { BookId = _entry.BookId });
            if (bookDetails.BookType != BookType.FuelCreditDiscountBook.ToString())
            {
                throw new Exception("Book type mismatch with the view.");
            }
            DiscountPerLitre = bookDetails.DiscountPerLitre.Value;
        }

        private void LoadEntry()
        {
            IdText.Text = _entry.Id.ToString();
            SerialText.Text = _entry.SerialNumber.ToString();
            DescriptionText.Text = _entry.Description;
            DateDatePicker.SelectedDate = DateTime.Parse(_entry.Date);
            DebitText.Text = _entry.Debit.ToString();
            AmountTextBox.Text = _entry.Amount.ToString();
            CreditText.Text = _entry.Credit.ToString();
            ItemTypeCombo.Text = _entry.ItemType;
            DriverCombo.Text = _entry.Driver;
            VehicleCombo.Text = _entry.Vehicle;
            LitreTextBox.Text = _entry.Litre.ToString();
            CreatedByText.Text = _entry.CreatedBy + " On " + _entry.CreatedOn;
            UpdatedByText.Text = _entry.UpdatedBy + " On " + _entry.UpdatedOn;

            if(!string.IsNullOrEmpty(AmountTextBox.Text))
            {
                CreditTextBlock.Visibility = Visibility.Visible;
                CreditTextBlock.Text = _entry.Credit.ToString();
                CreditText.Visibility = Visibility.Hidden;
                CreditText.Text = null;
                DebitText.Visibility = Visibility.Hidden;
            }
            else
            {
                AmountTextBox.Visibility = Visibility.Hidden;
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
        }

        private async void LoadComboBoxesAsync()
        {
            var itemTypes = await _mediator.Send(new ListItemTypesQuery());
            var drivers = await _mediator.Send(new ListDriversQuery { BookId = _entry.BookId });
            var vehicles = await _mediator.Send(new ListVehiclesQuery { BookId = _entry.BookId });

            foreach (var item in itemTypes.ItemTypes)
            {
                ItemTypeCombo.Items.Add(item);
            }
            foreach (var item in drivers.Drivers)
            {
                DriverCombo.Items.Add(item);
            }
            foreach (var item in vehicles.Vehicles)
            {
                VehicleCombo.Items.Add(item);
            }
        }

        private void NewVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            OldVehicle = false;
            NewVehicleButton.Visibility = Visibility.Hidden;
            OldVehicleButton.Visibility = Visibility.Visible;
            VehicleCombo.Visibility = Visibility.Hidden;
            VehicleText.Visibility = Visibility.Visible;
        }

        private void OldVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            OldVehicle = true;
            NewVehicleButton.Visibility = Visibility.Visible;
            OldVehicleButton.Visibility = Visibility.Hidden;

            VehicleCombo.Visibility = Visibility.Visible;
            VehicleText.Visibility = Visibility.Hidden;
        }

        private void OldDriverButton_Click(object sender, RoutedEventArgs e)
        {
            OldDriver = true;
            NewDriverButton.Visibility = Visibility.Visible;
            OldDriverButton.Visibility = Visibility.Hidden;

            DriverCombo.Visibility = Visibility.Visible;
            DriverText.Visibility = Visibility.Hidden;
        }

        private void NewDriverButton_Click(object sender, RoutedEventArgs e)
        {
            OldDriver = false;
            NewDriverButton.Visibility = Visibility.Hidden;
            OldDriverButton.Visibility = Visibility.Visible;
            DriverCombo.Visibility = Visibility.Hidden;
            DriverText.Visibility = Visibility.Visible;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            float? debit = null;
            float? credit = null;
            float? litre = null;
            float? amount = null;

            var debitConverted = float.TryParse(DebitText.Text, out float outDebit);
            if (!string.IsNullOrEmpty(DebitText.Text) && !debitConverted)
            {
                throw new Exception("Debit must be a number.");
            }
            else if (!string.IsNullOrEmpty(DebitText.Text))
            {
                debit = outDebit;
            }

            if (CreditText.Visibility == Visibility.Hidden)
            {
                var creditConverted = float.TryParse(CreditTextBlock.Text, out float outCredit);
                if (!string.IsNullOrEmpty(CreditTextBlock.Text) && !creditConverted)
                {
                    throw new Exception("Credit must be a number.");
                }
                else if (!string.IsNullOrEmpty(CreditTextBlock.Text))
                {
                    credit = outCredit;
                }
            }
            else
            {
                var creditConverted = float.TryParse(CreditText.Text, out float outCredit);
                if (!string.IsNullOrEmpty(CreditText.Text) && !creditConverted)
                {
                    throw new Exception("Credit must be a number.");
                }
                else if (!string.IsNullOrEmpty(CreditText.Text))
                {
                    credit = outCredit;
                }
            }

            var litreConverted = float.TryParse(LitreTextBox.Text, out float outLitre);
            if (!string.IsNullOrEmpty(LitreTextBox.Text) && !litreConverted)
            {
                throw new Exception("Litre must be a number.");
            }
            else if (!string.IsNullOrEmpty(LitreTextBox.Text))
            {
                litre = outLitre;
            }

            var amountConverted = float.TryParse(AmountTextBox.Text, out float outAmount);
            if (!string.IsNullOrEmpty(AmountTextBox.Text) && !amountConverted)
            {
                throw new Exception("Amount must be a number.");
            }
            else if (!string.IsNullOrEmpty(AmountTextBox.Text))
            {
                amount = outAmount;
            }


            string? driver = null;
            string? vehicle = null;
            if (OldDriver)
            {
                driver = DriverCombo.Text;
            }
            else
            {
                driver = DriverText.Text;
            }
            if (OldVehicle)
            {
                vehicle = VehicleCombo.Text;
            }
            else
            {
                vehicle = VehicleText.Text;
            }

            await _mediator.Send(new UpdateBookEntryCommand
            {
                BookEntryId = _entry.Id,
                Date = DateDatePicker.SelectedDate.ToString(),
                Description = DescriptionText.Text,
                Debit = debit,
                Credit = credit,
                ItemType = string.IsNullOrEmpty(ItemTypeCombo.Text) ? null : ItemTypeCombo.Text,
                Driver = driver,
                Vehicle = vehicle,
                Litre = litre,
                Amount = amount,
                UserId = LoginView.UserId.Value
            });
            _mainWindow.ShowBookEntries(_entry.BookId);
            MessageBox.Show("Entry successful!");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowBookEntries(_entry.BookId);
        }

        private void DebitText_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(DebitText.Text))

            {
                CreditText.Visibility = Visibility.Visible;
                AmountTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                CreditText.Visibility = Visibility.Hidden;
                AmountTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void CreditText_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(CreditText.Text))
            {
                DebitText.Visibility = Visibility.Visible;
                AmountTextBox.Visibility = Visibility.Visible;

            }
            else
            {
                DebitText.Visibility = Visibility.Hidden;
                AmountTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void LitreTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(LitreTextBox.Text))
            {
                CreditTextBlock.Text = AmountTextBox.Text;
            }
            else
            {
                if (!float.TryParse(LitreTextBox.Text, out float litre))
                {
                    throw new Exception("Litre must be number.");
                }
                if (!string.IsNullOrEmpty(AmountTextBox.Text))
                {
                    if (!float.TryParse(AmountTextBox.Text, out float amount))
                    {
                        throw new Exception("Amount must be number.");
                    }

                    CreditTextBlock.Text = (amount - litre * DiscountPerLitre).ToString();

                }
            }
        }

        private void AmountTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(AmountTextBox.Text))
            {
                DebitText.Visibility = Visibility.Visible;
                CreditTextBlock.Text = null;
                CreditTextBlock.Visibility = Visibility.Hidden;
                CreditText.Visibility = Visibility.Visible;
            }
            else
            {
                DebitText.Visibility = Visibility.Hidden;
                CreditTextBlock.Visibility = Visibility.Visible;
                CreditText.Visibility = Visibility.Hidden;
                if (!string.IsNullOrEmpty(LitreTextBox.Text))
                {
                    if (!float.TryParse(LitreTextBox.Text, out float litre))
                    {
                        throw new Exception("Litre must be a number.");
                    }
                    if (!float.TryParse(AmountTextBox.Text, out float amount))
                    {
                        throw new Exception("Amount must be a number.");
                    }
                    CreditTextBlock.Text = (amount - litre * DiscountPerLitre).ToString();
                }
            }
        }
    }
}
