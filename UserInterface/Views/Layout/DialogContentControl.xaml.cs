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

namespace Bk.UserInterface.Views.Layout
{
    /// <summary>
    /// Interaction logic for DialogContentControl.xaml
    /// </summary>
    public partial class DialogContentControl : UserControl
    {
        private readonly MainWindow _mainWindow;
        public DialogContentControl(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.DialogGridContent.Content = null;
        }
    }
}
