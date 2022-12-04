using Bk.Domain.Entities;
using Bk.Domain.Enums;
using Bk.Infrastructure.Context;
using Bk.Infrastructure.Queries.BookEntries;
using Bk.Infrastructure.Queries.Books;
using Bk.Infrastructure.Queries.Users;
using Bk.UserInterface.Views.Book;
using Bk.UserInterface.Views.Book.BookEntry;
using Bk.UserInterface.Views.Book.CreateBook;
using Bk.UserInterface.Views.Home;
using Bk.UserInterface.Views.Layout;
using Bk.UserInterface.Views.Layout.LayoutButtonControls;
using Bk.UserInterface.Views.Login;
using Bk.UserInterface.Views.Settings;
using Bk.UserInterface.Views.Settings.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

namespace Bk.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TopBarControl topBarControl;
        private readonly LoginView loginView;
        private readonly IMediator _mediator;
        public MainWindow(AppDbContext context, IMediator mediator)
        {

            context.Database.Migrate();
            InitializeComponent();
            _mediator = mediator;
            topBarControl = new TopBarControl(mediator, this);
            loginView = new LoginView(mediator, this);
            MainContent.Content = loginView;
        }

        public void Logout()
        {
            Title = "Book Keeping";
            MainContent.Content = loginView;
            TopBarContent.Content = null;
            loginView.Logout();
        }

        public void AfterLogin(string userName)
        {
            Title = "Book Keeping : " + userName;
            TopBarContent.Content = topBarControl;
            Home();
        }

        public void Home()
        {
            MainContent.Content = new HomeView(_mediator, this);
            topBarControl.ResetContents();
            topBarControl.TitleText.Text = "Home";
            topBarControl.ButtonsContentControl.Content = new CreateBookButtonControl(this);
        }

        public void CreateBook()
        {
            MainContent.Content = new CreateBookView(_mediator, this);
            topBarControl.ResetContents();
            topBarControl.HomeButtonContentControl.Content = new HomeButtonControl(this);
            topBarControl.TitleText.Text = "Create Book";            
        }

        public async void ShowBookEntries(int bookId)
        {
            var book = await _mediator.Send(new GetBookByIdQuery { BookId = bookId });
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            object control = null;
            switch (book.BookType)
            {
                case nameof(BookType.SimpleBook):

                case nameof(BookType.EmployeeBook):

                case nameof(BookType.CreditBook):
                    control = new BookEntriesView(_mediator, book.Id, this);
                    break;

                case nameof(BookType.FuelCreditBook):
                    control = new FCBookEntriesView(_mediator, book.Id, this);
                    break;

                case nameof(BookType.FuelCreditDiscountBook):
                    break;
            }
            if(control == null)
            {
                throw new Exception("Encountered book without view.");
            }
            MainContent.Content = control;


            topBarControl.ResetContents();
            topBarControl.ResetContents();
            topBarControl.HomeButtonContentControl.Content = new HomeButtonControl(this);
            topBarControl.TitleText.Text = book.Name;
            topBarControl.ButtonsContentControl.Content = new BookDetailsControl(book.Id, this);
        }

        public async void ShowBookEntryDetails(BookEntryResponse entryResponse)
        {
            var bookDetails = await GetBookDetails(entryResponse.BookId);
            switch (bookDetails.BookType)
            {
                case nameof(BookType.SimpleBook):

                case nameof(BookType.EmployeeBook):

                case nameof(BookType.CreditBook):
                    MainContent.Content = new BookEntryDetailsControl(_mediator, entryResponse, this);
                    break;

                case nameof(BookType.FuelCreditBook):
                    break;

                case nameof(BookType.FuelCreditDiscountBook):
                    break;
            }

            topBarControl.ResetContents();
            topBarControl.HomeButtonContentControl.Content = new BackButtonControl(entryResponse.BookId, this);
            topBarControl.TitleText.Text = bookDetails.Name;
        }

        public async void InsertBookEntry(int bookId)
        {
            var bookDetails = await GetBookDetails(bookId);
            object control = null;

            switch (bookDetails.BookType)
            {
                case nameof(BookType.SimpleBook):

                case nameof(BookType.EmployeeBook):

                case nameof(BookType.CreditBook):
                    control = new CreateBookEntryView(bookId, this, _mediator);
                    break;

                case nameof(BookType.FuelCreditBook):
                    control = new FCCreateBookEntryView(_mediator, bookId, this);
                    break;

                case nameof(BookType.FuelCreditDiscountBook):
                    break;
            }
            if (control == null)
            {
                throw new Exception("Encountered book without view.");
            }

            MainContent.Content = control;
            topBarControl.ResetContents();
            topBarControl.TitleText.Text = "Inserting to " + bookDetails.Name;
            topBarControl.HomeButtonContentControl.Content = new BackButtonControl(bookDetails.Id, this);
        }

        public void UserDetails(UserResponse user)
        {
            MainContent.Content = new UserDetailsView(_mediator, this, user);
            topBarControl.ResetContents();
            topBarControl.HomeButtonContentControl.Content = new HomeButtonControl(this);
            topBarControl.TitleText.Text = "User Details";
        }

        

        

        public async void UpdateBookDetails(int bookId)
        {
            MainContent.Content = new BookDetailsView(this, _mediator, bookId);
            var bookDetails = await GetBookDetails(bookId);
            topBarControl.ResetContents();
            topBarControl.TitleText.Text = "Book Detils : " + bookDetails.Name;
            topBarControl.HomeButtonContentControl.Content = new BackButtonControl(bookId, this);
        }

        public void BackToBook(int bookId)
        {
            ShowBookEntries(bookId);
        }

        private async Task<BookResponse> GetBookDetails(int bookId)
        {
            var result = await _mediator.Send(new GetBookByIdQuery { BookId = bookId });
            if (result == null)
            {
                throw new Exception("Book not found");
            }
            return result;
        }

        public void Settings()
        {
            MainContent.Content = new SettingsView(this);
            topBarControl.ResetContents();
            topBarControl.HomeButtonContentControl.Content = new HomeButtonControl(this);
            topBarControl.TitleText.Text = "Settings";
        }

        public void UserList()
        {
            MainContent.Content = new ListUsersView(_mediator, this);
            topBarControl.ResetContents();
            topBarControl.TitleText.Text = "Users";
            topBarControl.HomeButtonContentControl.Content = new HomeButtonControl(this);
            topBarControl.ButtonsContentControl.Content = new UserCreateButtonControl(this);
        }

        public void CreateUser()
        {
            MainContent.Content = new CreateUserView(_mediator, this);
            topBarControl.ResetContents();
            topBarControl.HomeButtonContentControl.Content = new HomeButtonControl(this);
            topBarControl.TitleText.Text = "Create User";
        }
    }
}
