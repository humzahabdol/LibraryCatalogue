using LibraryOrganizerDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using LibraryCatalogue.UI.Librarian;
using LibraryCatalogue.Helpers;
using LibraryCatalogue.UI.Patron;
using Search;
using Unity;

namespace LibraryCatalogue.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISqlDbRepository _sqlDbRepository;
        private readonly ILibrarianHelper _librarianHelper;
        private readonly ISearchClass _searchClass;

        public MainWindow(ISqlDbRepository sqlDbRepository, ILibrarianHelper librarianHelper, ISearchClass searchClass)
        {
            _sqlDbRepository = sqlDbRepository;
            _librarianHelper = librarianHelper;
            _searchClass = searchClass;
            InitializeComponent();
            LoadDB();


        }

        private void LoadDB()
        {
            //Database.SetInitializer<LibraryContext>(new LibraryInitializer());
            

                //TODO: How to do dependency inject for WPF/MVVM
                var t = _sqlDbRepository.GetLibrarians();
                var z = _sqlDbRepository.GetBooks();

        }

        private void Btn_Patron_Click(object sender, RoutedEventArgs e)
        {
            PatronSearch pS = new PatronSearch(_sqlDbRepository,_librarianHelper,_searchClass);
            this.Content = pS;
        }

        private void Btn_LibrarianLogin_Click(object sender, RoutedEventArgs e)
        {

            Login login = new Login(_librarianHelper,_searchClass,_sqlDbRepository);
            
            this.Content = login;


            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new Uri("login.xaml",UriKind.RelativeOrAbsolute));

            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new Menu(_sqlDbRepository,_librarianHelper,_searchClass);
        }
    }
}
