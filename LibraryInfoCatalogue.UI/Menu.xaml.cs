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

namespace LibraryInfoCatalogue.UI
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            SearchBook searchBook = new SearchBook(false);
            frm_menu.Content = searchBook;
        }
        private void Hide()
        {
            foreach (Control ctl in containerCanvas.Children)
            {
                if (ctl.GetType() == typeof(Button))
                    ((Button)ctl).Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            Login login = new Login();
            frm_menu.Content = login;
        }
    }
}
