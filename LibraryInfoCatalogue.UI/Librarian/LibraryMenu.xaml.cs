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
using LibraryInfoCatalogue.Helper.BusinessClass;
using LibraryInfoCatalogue.UI.Librarian;

namespace LibraryInfoCatalogue.UI
{
    /// <summary>
    /// Interaction logic for LibraryMenu.xaml
    /// </summary>
    public partial class LibraryMenu : Page
    {
        private readonly ViewHelper _viewHelper;
        public LibraryMenu()
        {
            _viewHelper = new ViewHelper();
            InitializeComponent();
            Home home = new Home();
            frm_libraryMenu.Content = home;
        }

        private void Btn_searchBook_Click(object sender, RoutedEventArgs e)
        {
            SearchBook searchBook = new SearchBook(true);
            frm_libraryMenu.Content = searchBook;
        }

        private void Btn_logout_Click(object sender, RoutedEventArgs e)
        {
            _viewHelper.Hide(containerCanvas);
            frm_libraryMenu.Visibility = Visibility.Collapsed;
            Menu menu = new Menu();
            frm_gotomenu.Content = menu;
        }

        private void Btn_home_Click(object sender, RoutedEventArgs e)
        {
            
            Home home = new Home();
            frm_libraryMenu.Content = home;
        }
    }
}
