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
using LibraryCatalogue.Entities;
using LibraryCatalogue.Helpers;
using LibraryCatalogue.UI.Librarian;
using Search;

namespace LibraryCatalogue.UI
{
    /// <summary>
    /// Interaction logic for SearchBook.xaml
    /// </summary>
    public partial class SearchBook : Page
    {
        private readonly ISearchClass _searchClass;
        private readonly ILibrarianHelper _librarianHelper;
        private readonly ISqlDbRepository _sqlDbRepository;
        private bool _isLibrarian;
        public SearchBook(ISearchClass searchClass, ILibrarianHelper librarianHelper, ISqlDbRepository sqlDbRepository,bool isLibrarian)
        {
            _searchClass = searchClass;
            _librarianHelper = librarianHelper;
            _sqlDbRepository = sqlDbRepository;
            InitializeComponent();
            _isLibrarian = isLibrarian;
            if (_isLibrarian)
            {
                lbl_whoami.Content = "Librarian Search";
            }
            else
            {
                lbl_whoami.Content = "Patron Search";

            }
        }

        private void Btn_libSearch_Click(object sender, RoutedEventArgs e)
        {
            //lstview_book.SelectedItem = null;
            var booksFound = _librarianHelper.FilterBooks(txtbx_keywords.Text);
            lbl_NotFound.Visibility = booksFound.Count == 0 ? Visibility.Visible : Visibility.Hidden;
            if (lbl_NotFound.Visibility == Visibility.Visible)
            {
                txtbox_Description.Text = "";
                txtbox_FirstName.Text = "";
                txtbox_ISBN.Text = "";
                txtbox_authorLastName.Text = "";
                txtbox_bookID.Text = "";
                txtbox_language.Text = "";
                txtbox_numberOfPages.Text = "";
                txtbox_publisher.Text = "";
                txtbox_subject.Text = "";
                txtbox_title.Text = "";
                txtbox_yearpublished.Text = "";
                txtbox_available.Text = "";
            }

            lstview_book.ItemsSource = booksFound;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var books = _sqlDbRepository.GetBooks();

            lstview_book.ItemsSource = books;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            if (_isLibrarian)
            {
                frm_search.Content = new LibrarianMenu(_searchClass, _librarianHelper,_sqlDbRepository);
            }
            else
            {
                frm_search.Content = new Menu(_sqlDbRepository, _librarianHelper, _searchClass);

            }
        }

        private void Hide()
        {
            lbl_whoami.Visibility = Visibility.Hidden;
            lstview_book.Visibility = Visibility.Hidden;
            btn_libSearch.Visibility = Visibility.Hidden;
            lbl_search.Visibility = Visibility.Hidden;
            btn_back.Visibility = Visibility.Hidden;
            txtbx_keywords.Visibility = Visibility.Hidden;
            lbl_NotFound.Visibility = Visibility.Hidden;
            txtbox_Description.Visibility = Visibility.Hidden;
            txtbox_FirstName.Visibility = Visibility.Hidden;
            txtbox_ISBN.Visibility = Visibility.Hidden;
            txtbox_authorLastName.Visibility = Visibility.Hidden;
            txtbox_bookID.Visibility = Visibility.Hidden;
            txtbox_language.Visibility = Visibility.Hidden;
            txtbox_numberOfPages.Visibility = Visibility.Hidden;
            txtbox_publisher.Visibility = Visibility.Hidden;
            txtbox_subject.Visibility = Visibility.Hidden;
            txtbox_title.Visibility = Visibility.Hidden;
            txtbox_yearpublished.Visibility = Visibility.Hidden;
            txtbox_available.Visibility = Visibility.Hidden;
            lbl_ISBN.Visibility = Visibility.Hidden;
            lbl_NumPages.Visibility = Visibility.Hidden;
            lbl_Title.Visibility = Visibility.Hidden;
            lbl_authorLastName.Visibility = Visibility.Hidden;
            lbl_available.Visibility = Visibility.Hidden;
            lbl_dID.Visibility = Visibility.Hidden;
            lbl_description.Visibility = Visibility.Hidden;
            lbl_language.Visibility = Visibility.Hidden;
            lbl_firstName.Visibility = Visibility.Hidden;
            lbl_publisher.Visibility = Visibility.Hidden;
            lbl_yearPublished.Visibility = Visibility.Hidden;
            lbl_subject.Visibility = Visibility.Hidden;


        }

        private void Lstview_book_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book book = (Book)lstview_book.SelectedItem;
            if (book != null)
            {
                txtbox_Description.Text = book.Description;
                txtbox_FirstName.Text = book.FirstName;
                txtbox_ISBN.Text = book.ISBN;
                txtbox_authorLastName.Text = book.LastName;
                txtbox_bookID.Text = book.BookID.ToString();
                txtbox_language.Text = book.Language;
                txtbox_numberOfPages.Text = book.NumPages.ToString();
                txtbox_publisher.Text = book.Publisher;
                txtbox_subject.Text = book.Subject;
                txtbox_title.Text = book.Title;
                txtbox_yearpublished.Text = book.YearPublished;

                //check amount available
                var checkOutLogsList = _sqlDbRepository.GetCheckOutLogs();
                var checkout = checkOutLogsList.Count(x => x.BookID == book.BookID);
                var totalAvailable = book.NumberOfCopies - checkout;
                txtbox_available.Text = totalAvailable.ToString();
            }
            
        }
    }
}
