using Bk.UserInterface.Views.Layout;
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

namespace Bk.UserInterface.Views.Book.ListBooks
{
    /// <summary>
    /// Interaction logic for ListBooksPageControl.xaml
    /// </summary>
    public partial class PageControl : UserControl
    {
        private readonly IPageControl _pageControl;
        public PageControl(object pageControl)
        {
            InitializeComponent();
            _pageControl = (IPageControl)pageControl;
        }

        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            _pageControl.First();
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            _pageControl.Previous();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            _pageControl.Next();
        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            _pageControl.Last();
        }
    }
}
