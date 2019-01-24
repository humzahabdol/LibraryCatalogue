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
using Search;

namespace LibraryCatalogue.UI.Patron
{
    /// <summary>
    /// Interaction logic for PatronSearch.xaml
    /// </summary>
    public partial class PatronSearch : Page
    {
        private readonly ISqlDbRepository _sqlDbRepository;
        private readonly ILibrarianHelper _librarianHelper;
        private readonly ISearchClass _searchClass;
        public PatronSearch(ISqlDbRepository sqlDbRepository, ILibrarianHelper librarianHelper, ISearchClass searchClass)
        {
            _sqlDbRepository = sqlDbRepository;
            _librarianHelper = librarianHelper;
            _searchClass = searchClass;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(_sqlDbRepository, _librarianHelper, _searchClass);
            this.NavigationService.Navigate(main);
        }
    }
}
