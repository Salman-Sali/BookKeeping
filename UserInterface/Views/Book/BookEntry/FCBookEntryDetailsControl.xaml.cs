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

namespace Bk.UserInterface.Views.Book.BookEntry
{
    /// <summary>
    /// Interaction logic for FCBookEntryDetailsControl.xaml
    /// </summary>
    public partial class FCBookEntryDetailsControl : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly IMediator _mediator;
        private readonly BookEntryResponse _entry;
        private bool OldDriver;
        private bool OldVehicle;
        public FCBookEntryDetailsControl(IMediator mediator, BookEntryResponse entry, MainWindow mainWindow)
        {
            InitializeComponent();
            _mediator = mediator;
            _mainWindow = mainWindow;
            _entry = entry;
            OldDriver = true;
            OldVehicle = true;
            LoadComboBoxesAsync();
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
            ItemTypeCombo.Text = _entry.ItemType;
            DriverCombo.Text = _entry.Driver;
            VehicleCombo.Text = _entry.Vehicle;
            LitreTextBox.Text = _entry.Litre.ToString();
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

            var litreConverted = float.TryParse(LitreTextBox.Text, out float outLitre);
            if (!string.IsNullOrEmpty(LitreTextBox.Text) && !litreConverted)
            {
                throw new Exception("Litre must be a number.");
            }
            else if (!string.IsNullOrEmpty(LitreTextBox.Text))
            {
                litre = outLitre;
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
                ItemType = ItemTypeCombo.Text,
                Driver = driver,
                Vehicle = vehicle,
                Litre = litre,
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
            }
            else
            {
                CreditText.Visibility = Visibility.Hidden;
            }
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
    }
}
