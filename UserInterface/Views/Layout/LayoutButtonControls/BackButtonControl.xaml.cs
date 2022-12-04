using Bk.Infrastructure.Queries.Books;
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
    /// Interaction logic for BackButtonControl.xaml
    /// </summary>
    public partial class BackButtonControl : UserControl
    {
        private readonly int _book;
        private readonly MainWindow _mainWindow;
        public BackButtonControl(int book, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _book = book;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.BackToBook(_book);
        }
    }
}
