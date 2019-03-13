using LibraryInfoCatalogue.Helper.BusinessClass;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using LibraryOrganizerDB;
using LibraryOrganizerDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LibraryInfoCatalogue.UI.Librarian
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private readonly BookRepository _bookRepository;
        private readonly LibrarianRepository _librarianRepository;
        private readonly AuthorRepository _authorRepository;
        private readonly CardholderRepository _cardholderRepository;
        private readonly ViewHelper _viewHelper;
        private readonly LibrarianHelper _librarianHelper;
        private readonly CheckoutLogRepository _checkoutLogRepository;
        private readonly BookHelper _bookHelper;
        public Home()
        {
            InitializeComponent();
            _bookRepository = new BookRepository();
            _librarianRepository = new LibrarianRepository();
            _authorRepository = new AuthorRepository();
            _cardholderRepository = new CardholderRepository();
            _checkoutLogRepository = new CheckoutLogRepository();
            _librarianHelper = new LibrarianHelper();
            _bookHelper = new BookHelper();
            _viewHelper = new ViewHelper();
            SetUp();


        }

        private void SetUp()
        {
            var overdue = OverDueBooks();

            listview_books.ItemsSource = ConvertBooktoSearchDisplay(overdue);
            listview_authors.ItemsSource = ConvertAuthorToSearchDisplay(_authorRepository.GetAll());
            listview_cardholders.ItemsSource = ConvertCardholderToSearchDisplay(_cardholderRepository.GetAll());
            listview_librarians.ItemsSource = ConvertLibrarianToSearchDisplay(_librarianRepository.GetAll());
        }

        private List<Book> OverDueBooks()
        {
            List<Book> books = new List<Book>();
            var checkoutlogs = _checkoutLogRepository.GetAll();
            foreach (var checkoutlog in checkoutlogs)
            {
                if (DateTime.Now > checkoutlog.CheckOutDate.AddDays(30))
                {
                    books.Add(_bookRepository.Find(checkoutlog.BookID));
                }
            }

            return books;
        }
        private List<SearchDisplay> ConvertLibrarianToSearchDisplay(List<LibraryOrganizerDB.Entities.Librarian> librarians)
        {
            List<SearchDisplay> searchDisplays = new List<SearchDisplay>();
            foreach (var librarian in librarians)
            {
                var searchDisplay = new SearchDisplay();
                searchDisplay.Display = librarian.ToString();
                searchDisplay.Person = librarian.Person;
                searchDisplays.Add(searchDisplay);
            }

            return searchDisplays.OrderBy(x=>x.Person.LastName).OrderBy(x=>x.Person.FirstName).ToList();
        }
        private List<SearchDisplay> ConvertCardholderToSearchDisplay(List<Cardholder> cardholders)
        {
            List<SearchDisplay> searchDisplays = new List<SearchDisplay>();
            foreach (var ch in cardholders)
            {
                var checkoutlog = _checkoutLogRepository.GetAll().Where(x => x.CardholderID == ch.ID).Select(x => x)
                    .FirstOrDefault();
                var searchDisplay = new SearchDisplay();
                var hasOverDue = "No";

                searchDisplay.Display = ch.ToString();
                searchDisplay.Person = ch.Person;
                if (checkoutlog != null)
                {
                    //TODO: Check if this is returning false
                    if (_librarianHelper.CanCheckOut(checkoutlog.Book, ch.ID))
                    {
                        hasOverDue = "Yes";
                    }
                    searchDisplay.Display += $"Book ID: {checkoutlog.BookID}, " +
                                            $"ISBN: {checkoutlog.Book.ISBN}, " +
                                            $"Title: {checkoutlog.Book.Title}, " +
                                            $"Check Out Date: {checkoutlog.CheckOutDate.ToString("MM/dd/yyyy")}," +
                                            $"Has Over Due: {hasOverDue}";
                    
                    searchDisplay.Book = checkoutlog.Book;
                }
                searchDisplays.Add(searchDisplay);
            }

            return searchDisplays.OrderBy(x=>x.Person.LastName).OrderBy(x=>x.Person.FirstName).ToList();
        }
        private List<SearchDisplay> ConvertAuthorToSearchDisplay(List<Author> authors)
        {
            List<SearchDisplay> searchDisplays = new List<SearchDisplay>();
            foreach (var author in authors)
            {
                var book = _bookRepository.GetAll().Where(x => x.AuthorID == author.ID).FirstOrDefault();
                var searchDisplay = new SearchDisplay();
                if (book != null)
                {
                    searchDisplay.Display = author.ToString() +
                                            $", Book Id: {book.BookID}, ISBN: {book.ISBN}, Title: {book.Title}, Publisher: {book.Publisher}";
                    searchDisplay.Book = book;
                    searchDisplay.Person = author.Person;
                    searchDisplays.Add(searchDisplay);
                }
                
            }

            return searchDisplays.OrderBy(x => x.Person.LastName)
                .OrderBy(x => x.Person.FirstName)
                .OrderBy(x => x.Book.Title).ToList();
        }
        private List<SearchDisplay> ConvertBooktoSearchDisplay(List<Book> books)
        {

            List<SearchDisplay> searchDisplays = new List<SearchDisplay>();
            foreach (var book in books)
            {
                var checkoutlog = _checkoutLogRepository.GetAll().Where(x => x.BookID == book.BookID).FirstOrDefault();
                var searchDisplay = new SearchDisplay();
                searchDisplay.Book = book;
                searchDisplay.CheckOutLogDate = checkoutlog.CheckOutDate;
                searchDisplay.Display = $"Id:{book.BookID}, " +
                                        $"ISBN: {book.ISBN}, " +
                                        $"Title: {book.Title}, " +
                                        $"Check Out Date: {checkoutlog.CheckOutDate.ToString("MM/dd/yyyy")}, " +
                                        $"Cardholder Id: {checkoutlog.Cardholder.ID}, " +
                                        $"Cardholder Full Name: {checkoutlog.Cardholder.Person.FirstName} {checkoutlog.Cardholder.Person.LastName}, " +
                                        $"Library Card Id: {checkoutlog.Cardholder.LibraryCardID}, " +
                                        $"Phone: {checkoutlog.Cardholder.Phone}";
                searchDisplays.Add(searchDisplay);
            }

            return searchDisplays.OrderBy(x => x.CheckOutLogDate)
                                .OrderBy(x => x.Book.Title)
                                .ToList();
        }
    }
}
