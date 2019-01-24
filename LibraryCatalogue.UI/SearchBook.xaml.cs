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
        }

        private void Btn_libSearch_Click(object sender, RoutedEventArgs e)
        {
            var t = _librarianHelper.FilterBooks(txtbx_keywords.Text);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var books = _sqlDbRepository.GetBooks();
            lstbx_book.DataContext = books;
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
            lstbx_book.Visibility = Visibility.Hidden;
            btn_libSearch.Visibility = Visibility.Hidden;
            lbl_search.Visibility = Visibility.Hidden;
            btn_back.Visibility = Visibility.Hidden;
            txtbx_keywords.Visibility = Visibility.Hidden;

        }
    }
}
