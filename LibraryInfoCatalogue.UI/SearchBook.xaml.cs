using System;
using System.Windows;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using System.Windows.Controls;
using System.Windows.Navigation;
using LibraryInfoCatalogue.Helper.BusinessClass;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.UI
{
    /// <summary>
    /// Interaction logic for SearchBook.xaml
    /// </summary>
    public partial class SearchBook : Page
    {
        private readonly BookRepository _bookRepository;
        private readonly BookHelper _bookHelper;
        private readonly bool _isLibrarian;
        public SearchBook(bool isLibrarian)
        {
            InitializeComponent();
            _bookRepository = new BookRepository();
            _bookHelper = new BookHelper();
            _isLibrarian = isLibrarian;
            SetSelection();
        }

        private void SetSelection()
        {
            var books = _bookRepository.GetAll();
            books.ForEach(x => { x.Available = _bookHelper.CanCheckOutBook(x); });
            listviewSearch.ItemsSource = books;
            if (_isLibrarian)
            {
                lbl_whoAmI.Content = "Librarian Search";
            }
            else
            {
                lbl_whoAmI.Content = "Patron Search";
            }

            lbl_noneFound.Visibility = Visibility.Hidden;
        }

        private void ListviewSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var book = (Book) listviewSearch.SelectedItem;
            if (book != null)
            {
                txtbx_bookId.Text = book.BookID.ToString();
                txtbx_authorFullName.Text = $"{book.Author.Person.FirstName} {book.Author.Person.LastName}";
                txtbx_description.Text = book.Description;
                txtbx_isbn.Text = book.ISBN;
                txtbx_language.Text = book.Language;
                txtbx_numOfCopies.Text = book.NumberOfCopies.ToString();
                txtbx_publisher.Text = book.Publisher;
                txtbx_numPages.Text = book.NumPages.ToString();
                txtbx_subject.Text = book.Subject;
                txtbx_title.Text = book.Title;
                txtbx_yearPublished.Text = book.YearPublished;
                txtbx_totalAvailable.Text = _bookHelper.CountAvailable(book).ToString();
            }

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var text = txtbx_searchBook.Text;
            var listBooks = _bookHelper.FindBooksSearch(txtbx_searchBook.Text);
            listBooks.ForEach(x => { x.Available = _bookHelper.CanCheckOutBook(x); });
            lbl_noneFound.Visibility = listBooks.Count > 0 ? Visibility.Hidden : Visibility.Visible;
            listviewSearch.ItemsSource = listBooks;
            ClearTextBox();

            txtbx_searchBook.Text = text;
        }

        private void Btn_showAll_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
            SetSelection();
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            if (_isLibrarian)
            {
                LibraryMenu libraryMenu = new LibraryMenu();
                frm_searchbook.Content = libraryMenu;

            }
            else
            {
                Menu menu = new Menu();
                frm_searchbook.Content = menu;

            }

        }

        private void ClearTextBox()
        {
            foreach (Control ctl in containerCanvas.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            }
        }

        private void Hide()
        {
            foreach (Control ctl in containerCanvas.Children)
            {
                
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(ListView))
                    ((ListView)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Button))
                    ((Button)ctl).Visibility = Visibility.Collapsed;
            }
        }
    }
}
