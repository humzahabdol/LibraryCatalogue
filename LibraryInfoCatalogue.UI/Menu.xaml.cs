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

namespace LibraryInfoCatalogue.UI
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private readonly ViewHelper _viewHelper;
        public Menu()
        {
            InitializeComponent();
            _viewHelper = new ViewHelper();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewHelper.Hide(containerCanvas);
            SearchBook searchBook = new SearchBook(false);
            frm_menu.Content = searchBook;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _viewHelper.Hide(containerCanvas);
            Login login = new Login();
            frm_menu.Content = login;
        }
    }
}
