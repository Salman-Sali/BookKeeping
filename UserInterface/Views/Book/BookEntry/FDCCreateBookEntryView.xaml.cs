using BK.Application.Commands.BookEntries;
using Bk.Infrastructure.Queries.BookEntries;
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
using Bk.Infrastructure.Queries.Books;
using Bk.Domain.Enums;

namespace Bk.UserInterface.Views.Book.BookEntry
{
    /// <summary>
    /// Interaction logic for FDCCreateBookEntryView.xaml
    /// </summary>
    public partial class FDCCreateBookEntryView : UserControl
    {
        private readonly IMediator _mediator;
        private readonly MainWindow _mainWindow;
        private readonly int BookId;
        private float DiscountPerLitre;
        private bool OldDriver;
        private bool OldVehicle;
        public FDCCreateBookEntryView(IMediator mediator, int bookId, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mediator = mediator;
            BookId = bookId;
            OldDriver = true;
            OldVehicle = true;
            DateDatePicker.SelectedDate = DateTime.Now;
            LoadBookDetails();
            LoadComboBoxes();
        }

        private async void LoadBookDetails()
        {
            var bookDetails = await _mediator.Send(new GetBookByIdQuery { BookId = BookId });
            if(bookDetails.BookType != BookType.FuelCreditDiscountBook.ToString())
            {
                throw new Exception("Book type mismatch with the view.");
            }
            DiscountPerLitre = bookDetails.DiscountPerLitre.Value;
        }

        private void LitreTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(LitreTextBox.Text))
            {

            }
            else
            {
                if(!float.TryParse(LitreTextBox.Text, out float litre))
                {
                    throw new Exception("Litre must be number.");
                }
                if (!string.IsNullOrEmpty(AmountTextBox.Text))
                {
                    if (!float.TryParse(AmountTextBox.Text, out float amount))
                    {
                        throw new Exception("Amount must be number.");
                    }

                    CreditText.Text = (amount - litre * DiscountPerLitre).ToString();

                }
            }
        }

        public async void LoadComboBoxes()
        {
            var itemTypes = await _mediator.Send(new ListItemTypesQuery());
            var drivers = await _mediator.Send(new ListDriversQuery { BookId = BookId });
            var vehicles = await _mediator.Send(new ListVehiclesQuery { BookId = BookId });

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

        private void DebitTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(DebitTextBox.Text))

            {
                CreditGrid.Visibility = Visibility.Visible;
                AmountGrid.Visibility = Visibility.Visible;
            }
            else
            {
                CreditGrid.Visibility = Visibility.Hidden;
                AmountGrid.Visibility = Visibility.Hidden;
            }
        }

        private void CreditTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(CreditTextBox.Text))
            {
                DebitGrid.Visibility = Visibility.Visible;
                AmountGrid.Visibility = Visibility.Visible;

            }
            else
            {
                DebitGrid.Visibility = Visibility.Hidden;
                AmountGrid.Visibility = Visibility.Hidden;
            }
        }
        private void AmountTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(AmountTextBox.Text))
            {
                DebitGrid.Visibility = Visibility.Visible;
                CreditText.Text = null;
                CreditText.Visibility =Visibility.Hidden;
                CreditTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                DebitGrid.Visibility = Visibility.Hidden;
                CreditText.Visibility = Visibility.Visible;
                CreditTextBox.Visibility = Visibility.Hidden;
                if(!string.IsNullOrEmpty(LitreTextBox.Text))
                {
                    if(!float.TryParse(LitreTextBox.Text, out float litre))
                    {
                        throw new Exception("Litre must be a number.");
                    }
                    if(!float.TryParse(AmountTextBox.Text, out float amount))
                    {
                        throw new Exception("Amount must be a number.");
                    }
                    CreditText.Text = (amount - litre * DiscountPerLitre).ToString();
                }
            }
        }

        private void NewDriverButton_Click(object sender, RoutedEventArgs e)
        {
            OldDriver = false;
            NewDriverButton.Visibility = Visibility.Hidden;
            OldDriverButton.Visibility = Visibility.Visible;
            DriverCombo.Visibility = Visibility.Hidden;
            DriverText.Visibility = Visibility.Visible;
        }

        private void OldDriverButton_Click(object sender, RoutedEventArgs e)
        {
            OldDriver = true;
            NewDriverButton.Visibility = Visibility.Visible;
            OldDriverButton.Visibility = Visibility.Hidden;

            DriverCombo.Visibility = Visibility.Visible;
            DriverText.Visibility = Visibility.Hidden;
        }

        private async void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            float? debit = null;
            float? credit = null;
            float? litre = null;
            float? amount = null;
            var debitConverted = float.TryParse(DebitTextBox.Text, out float outDebit);
            if (!string.IsNullOrEmpty(DebitTextBox.Text) && !debitConverted)
            {
                throw new Exception("Debit must be a number.");
            }
            else if (!string.IsNullOrEmpty(DebitTextBox.Text))
            {
                debit = outDebit;
            }

            if(CreditTextBox.Visibility == Visibility.Hidden)
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
            else
            {
                var creditConverted = float.TryParse(CreditTextBox.Text, out float outCredit);
                if (!string.IsNullOrEmpty(CreditTextBox.Text) && !creditConverted)
                {
                    throw new Exception("Credit must be a number.");
                }
                else if (!string.IsNullOrEmpty(CreditTextBox.Text))
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

            await _mediator.Send(new AddBookEntryCommand
            {
                BookId = BookId,
                Date = DateDatePicker.SelectedDate.ToString(),
                Description = DescriptionTextBox.Text,
                Debit = debit,
                Credit = credit,
                ItemType = ItemTypeCombo.Text,
                Driver = driver,
                Vehicle = vehicle,
                Litre = litre,
                Amount = amount,
                UserId = LoginView.UserId.Value
            });
            _mainWindow.ShowBookEntries(BookId);
            MessageBox.Show("Entry successful!");
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
    }
}
