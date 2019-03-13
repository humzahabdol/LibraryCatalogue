using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibraryInfoCatalogue.Helper.BusinessClass;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.UI.Librarian
{
    /// <summary>
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        private readonly BookRepository _bookRepository;
        private readonly BookHelper _bookHelper;
        private readonly ViewHelper _viewHelper;
        public BookPage()
        {
            InitializeComponent();
            lbl_notFound.Visibility = Visibility.Hidden;
            lbl_update.Visibility = Visibility.Hidden;
            _bookRepository = new BookRepository();
            _bookHelper = new BookHelper();
            _viewHelper = new ViewHelper();


        }



        private void Btn_addCopy_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txt_isbn.Text))
            {
                //TODO: Check if the string pass in is a number or change the text field to readonly
                int counter = 0;
                if (!string.IsNullOrEmpty(txt_numberofcopies.Text))
                {
                    counter = Convert.ToInt32(txt_numberofcopies.Text);
                }
                var numCopies = counter + 1;
                txt_numberofcopies.Text = numCopies.ToString();
            }

        }

        private void Btn_removeCopy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_isbn.Text))
            {
                //TODO: Check if the string pass in is a number or change the text field to readonly

                int counter = 0;
                if (!string.IsNullOrEmpty(txt_numberofcopies.Text))
                {
                    counter = Convert.ToInt32(txt_numberofcopies.Text);
                }
                var numCopies = counter - 1;
                txt_numberofcopies.Text = numCopies.ToString();
            }
        }

        private void Btn_submit_Click(object sender, RoutedEventArgs e)
        {

            var text = txt_search.Text;
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
            lstview_books.ItemsSource = displayList;
            lbl_notFound.Visibility = lstview_books.Items.Count > 0 ? Visibility.Hidden : Visibility.Visible;
            _viewHelper.ClearTextBox(containerCanvas);
            txt_search.Text = text;

        }

        private void Lstview_books_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var searchDisplay = (SearchDisplay)lstview_books.SelectedItem;
            if (searchDisplay != null)
            {
                txt_description.Text = searchDisplay.Book.Description;
                txt_isbn.Text = searchDisplay.Book.ISBN;
                txt_language.Text = searchDisplay.Book.Language;
                txt_numberofcopies.Text = searchDisplay.Book.NumberOfCopies.ToString();
                txt_publisher.Text = searchDisplay.Book.Publisher;
                txt_numpages.Text = searchDisplay.Book.NumPages.ToString();
                txt_subject.Text = searchDisplay.Book.Subject;
                txt_title.Text = searchDisplay.Book.Title;
                txt_yearpublished.Text = searchDisplay.Book.YearPublished;
            }
        }
        private void Btn_update_Click(object sender, RoutedEventArgs e)
        {
            lbl_update.Foreground = new SolidColorBrush(Colors.Red);
            bool isBookUpdated = false;
            lbl_update.Visibility = Visibility.Hidden;
            if (!string.IsNullOrEmpty(txt_isbn.Text))
            {
                if (string.IsNullOrEmpty(txt_numberofcopies.Text))
                {
                    lbl_update.Content = "No Value for number of copies.";
                    lbl_update.Foreground = new SolidColorBrush(Colors.Red);
                    lbl_update.Visibility = Visibility.Visible;
                    return;
                }
                int numOfCopies = Convert.ToInt32(txt_numberofcopies.Text);
                Book book = _bookHelper.FindBooksSearch(txt_isbn.Text).FirstOrDefault();
                //Business rule 1
                //If a book is being added and the ISBN number that was entered is already in the system
                //(use the searching functionality to determine this),
                //simply add the number of copies to the existing record
                //(there should only be one record for each ISBN).
                if (book != null)
                {
                    
                    if (numOfCopies > 0)
                    {
                        //Business rule 3
                        //If the resulting number of copies is less than the number
                        //of copies currently checked out, then cancel the update and
                        //tell the user to reenter the number of copies of books being
                        //removed from the system and inform them how many books are currently checked out.
                        if (_bookHelper.CheckoutCount(txt_isbn.Text) > numOfCopies)
                        {
                            lbl_update.Content =
                                $"Reenter the number of copies of the books being removed from the system there are {_bookHelper.CheckoutCount(txt_isbn.Text)} checkedout.";
                            lbl_update.Visibility = Visibility.Visible;

                            return;
                        }
                        else
                        {
                            book.Description = txt_description.Text;
                            book.Language = txt_language.Text;
                            book.NumPages = Convert.ToInt32(txt_numpages.Text);
                            book.Publisher = txt_publisher.Text;
                            book.Subject = txt_subject.Text;
                            book.Title = txt_title.Text;
                            book.YearPublished = txt_yearpublished.Text;
                            book.NumberOfCopies = Convert.ToInt32(txt_numberofcopies.Text);
                            isBookUpdated =  _bookRepository.Update(book);
                        }
                    }
                    //Business rule 4
                    //If the resulting number is 0, you can either leave the book
                    //in the system(it should not show up in patron-based searches),
                    //or remove the record completely(your choice).Allow the librarian
                    //to cancel the delete before it’s committed.
                    else if (numOfCopies == 0)
                    {
                        DeleteBook();
                    }
                    //Business rule 2
                    //If the resulting number of copies is less than zero,
                    //then cancel the update and tell the user to reenter
                    //the number of copies of books being removed from the system.
                    else
                    {
                        lbl_update.Content = "Cannot update number of copies cannot be less than 0.";
                        lbl_update.Visibility = Visibility.Visible;
                    }

                    if (isBookUpdated)
                    {
                        lbl_update.Foreground = new SolidColorBrush(Colors.Green);
                        lbl_update.Content = "Book Updated.";
                    }
                }
                else
                {
                    lbl_notFound.Visibility = Visibility.Visible;
                    lbl_notFound.Foreground = new SolidColorBrush(Colors.Red);
                    lbl_notFound.Content = "ISBN not found please add a new a book.";
                }
            }

        }
        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.Show();
            
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteBook();
        }
        private void DeleteBook()
        {
            var searchDisplay = (SearchDisplay)lstview_books.SelectedItem;
            DeleteBook deleteBook = new DeleteBook(searchDisplay.Book);
            deleteBook.Show();
            _viewHelper.ClearTextBox(containerCanvas);
        }
    }
}
