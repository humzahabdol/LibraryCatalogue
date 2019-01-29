using System.Windows;
using System.Windows.Controls;
using LibraryCatalogue.Helpers;
using Search;

namespace LibraryCatalogue.UI.Librarian
{
    /// <summary>
    /// Interaction logic for LibrarianMenu.xaml
    /// </summary>
    public partial class LibrarianMenu : Page
    {
        private readonly ISearchClass _searchClass;
        private readonly ILibrarianHelper _librarianHelper;
        private readonly ISqlDbRepository _sqlDbRepository;
        public LibrarianMenu(ISearchClass searchClass, ILibrarianHelper librarianHelper, ISqlDbRepository sqlDbRepository)
        {
            _searchClass = searchClass;
            _librarianHelper = librarianHelper;
            _sqlDbRepository = sqlDbRepository;
            InitializeComponent();
        }

        private void Btn_searchbook_Click(object sender, RoutedEventArgs e)
        {
            HideElements();
            frm_LibraryMenu.Content = new SearchBook(_searchClass,_librarianHelper,_sqlDbRepository,true);
        }

        private void Btn_checkinout_Click(object sender, RoutedEventArgs e)
        {
            HideElements();
            frm_LibraryMenu.Content = new CheckInOrOut(_searchClass,_librarianHelper,_sqlDbRepository);

        }

        private void Btn_addbook_Click(object sender, RoutedEventArgs e)
        {
            HideElements();
        }

        private void Btn_removebook_Click(object sender, RoutedEventArgs e)
        {
            HideElements();
        }

        private void Btn_updatebook_Click(object sender, RoutedEventArgs e)
        {
            HideElements();
        }

        private void HideElements()
        {
            btn_searchbook.Visibility = Visibility.Hidden;
            btn_checkinout.Visibility = Visibility.Hidden;
            btn_addbook.Visibility = Visibility.Hidden;
            btn_removebook.Visibility = Visibility.Hidden;
            btn_updatebook.Visibility = Visibility.Hidden;
            lbl_libmenutitle.Visibility = Visibility.Hidden;
        }
    }
}
