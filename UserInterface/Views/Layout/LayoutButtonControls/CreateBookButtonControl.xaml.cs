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

namespace Bk.UserInterface.Views.Layout.LayoutButtonControls
{
    /// <summary>
    /// Interaction logic for CreateBookButtonControl.xaml
    /// </summary>
    public partial class CreateBookButtonControl : UserControl
    {
        private readonly MainWindow _mainWindow;
        public CreateBookButtonControl(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void CreateBookButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.CreateBook();
        }
    }
}
