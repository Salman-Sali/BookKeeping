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
    /// Interaction logic for FCCreateBookEntryView.xaml
    /// </summary>
    public partial class FCCreateBookEntryView : UserControl
    {
        private readonly IMediator _mediator;
        private readonly MainWindow _mainWindow;
        private readonly int BookId;
        private bool OldDriver;
        private bool OldVehicle;
        public FCCreateBookEntryView(IMediator mediator, int bookId, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mediator = mediator;
            BookId = bookId;
            OldDriver = true;
            OldVehicle = true;
            DateDatePicker.SelectedDate = DateTime.Now;
            LoadComboBoxes();
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

            var debitConverted = float.TryParse(DebitTextBox.Text, out float outDebit);
            if (!string.IsNullOrEmpty(DebitTextBox.Text) && !debitConverted)
            {
                throw new Exception("Debit must be a number.");
            }
            else if (!string.IsNullOrEmpty(DebitTextBox.Text))
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
            if(OldVehicle)
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
                ItemType = string.IsNullOrEmpty(ItemTypeCombo.Text) ? null : ItemTypeCombo.Text,
                Driver = driver,
                Vehicle = vehicle,
                Litre = litre,
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
