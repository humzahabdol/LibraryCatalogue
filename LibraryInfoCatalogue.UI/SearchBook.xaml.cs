using System;
using System.Windows;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using System.Windows.Controls;
using System.Windows.Navigation;
using LibraryInfoCatalogue.Helper.BusinessClass;
using LibraryOrganizerDB.Entities;
using System.Collections.Generic;

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
        private readonly ViewHelper _viewHelper;
        public SearchBook(bool isLibrarian)
        {
            InitializeComponent();
            _bookRepository = new BookRepository();
            _bookHelper = new BookHelper();
            _viewHelper = new ViewHelper();
            _isLibrarian = isLibrarian;
            SetSelection();
        }

        private void ListviewSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var searchDisplay = (SearchDisplay) listviewSearch.SelectedItem;
            if (searchDisplay != null)
            {
                txtbx_bookId.Text = searchDisplay.Book.BookID.ToString();
                txtbx_authorFullName.Text = $"{searchDisplay.Book.Author.Person.FirstName} {searchDisplay.Book.Author.Person.LastName}";
                txtbx_description.Text = searchDisplay.Book.Description;
                txtbx_isbn.Text = searchDisplay.Book.ISBN;
                txtbx_language.Text = searchDisplay.Book.Language;
                txtbx_numOfCopies.Text = searchDisplay.Book.NumberOfCopies.ToString();
                txtbx_publisher.Text = searchDisplay.Book.Publisher;
                txtbx_numPages.Text = searchDisplay.Book.NumPages.ToString();
                txtbx_subject.Text = searchDisplay.Book.Subject;
                txtbx_title.Text = searchDisplay.Book.Title;
                txtbx_yearPublished.Text = searchDisplay.Book.YearPublished;
                txtbx_totalAvailable.Text = _bookHelper.CountAvailable(searchDisplay.Book).ToString();
            }

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SetDatagrid();
        }

        private void Btn_showAll_Click(object sender, RoutedEventArgs e)
        {
            _viewHelper.ClearTextBox(containerCanvas);
            SetSelection();
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            _viewHelper.Hide(containerCanvas);
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

        private void SetSelection()
        {
            SetDatagrid();
            if (_isLibrarian)
            {
                lbl_whoAmI.Content = "Librarian Search";
                btn_back.Visibility = Visibility.Collapsed;
            }
            else
            {
                lbl_whoAmI.Content = "Patron Search";

            }

            lbl_noneFound.Visibility = Visibility.Hidden;
        }

        private void SetDatagrid()
        {
            var text = txtbx_searchBook.Text;
            List<Book> books = new List<Book>();
            if (string.IsNullOrEmpty(text))
            {
                books = _bookRepository.GetAll();
            }
            else
            {
                books = _bookHelper.FindBooksSearch(text);
            }
            List<SearchDisplay> displayList = new List<SearchDisplay>();
            books.ForEach(x =>
            {
                SearchDisplay display = new SearchDisplay();
                display.Book = x;
                display.Available = _bookHelper.CanCheckOutBook(x);
                display.Display = $"Id: {x.BookID}, Title: {x.Title}, Author: {x.Author.Person.FirstName} {x.Author.Person.LastName}, Publisher: {x.Publisher}, Year Published: {x.YearPublished}";
                displayList.Add(display);
            });
            listviewSearch.ItemsSource = displayList;
            lbl_noneFound.Visibility = listviewSearch.Items.Count > 0 ? Visibility.Hidden : Visibility.Visible;
            _viewHelper.ClearTextBox(containerCanvas);
            txtbx_searchBook.Text = text;
        }
    }
}
