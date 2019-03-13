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
using System.Windows.Shapes;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.UI.Librarian
{
    /// <summary>
    /// Interaction logic for DeleteBook.xaml
    /// </summary>
    public partial class DeleteBook : Window
    {
        private readonly BookRepository _bookRepository;
        private readonly Book _book;
        public DeleteBook(Book book)
        {
            _bookRepository = new BookRepository();
            _book = book;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            txt_isbn.Text = _book.ISBN;
            txt_description.Text = _book.Description;
            txt_language.Text = _book.Language;
            txt_numpages.Text = _book.NumPages.ToString();
            txt_publisher.Text = _book.Publisher;
            txt_subject.Text = _book.Subject;
            txt_title.Text = _book.Title;
            txt_yearpublished.Text = _book.YearPublished;
        }
        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var book = _bookRepository.GetAll().Where(x => x.ISBN == txt_isbn.Text).FirstOrDefault();
            _bookRepository.Delete(book.BookID);
            this.Close();
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
