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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private readonly ISqlDbRepository _sqlDbRepository;
        private readonly ILibrarianHelper _librarianHelper;
        private readonly ISearchClass _searchClass;
        public Menu(ISqlDbRepository sqlDbRepository, ILibrarianHelper librarianHelper, ISearchClass searchClass)
        {
            _sqlDbRepository = sqlDbRepository;
            _librarianHelper = librarianHelper;
            _searchClass = searchClass;
            InitializeComponent();
        }

        private void Btn_Patron_Click(object sender, RoutedEventArgs e)
        {
            HideElement();
            MainMenu.Content = new SearchBook(_searchClass, _librarianHelper, _sqlDbRepository,false);
        }

        private void Btn_LibrarianLogin_Click(object sender, RoutedEventArgs e)
        {
            HideElement();
            MainMenu.Content = new Login(_librarianHelper,_searchClass,_sqlDbRepository);
        }

        private void HideElement()
        {
            btn_Patron.Visibility = Visibility.Hidden;
            btn_LibrarianLogin.Visibility = Visibility.Hidden;
            btn_Exit.Visibility = Visibility.Hidden;
        }
    }
}
