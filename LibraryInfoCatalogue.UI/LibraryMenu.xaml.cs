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
    /// Interaction logic for LibraryMenu.xaml
    /// </summary>
    public partial class LibraryMenu : Page
    {
        public LibraryMenu()
        {
            InitializeComponent();
        }

        private void Btn_searchBook_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            SearchBook searchBook = new SearchBook(true);
            frm_libraryMenu.Content = searchBook;
        }

        private void Hide()
        {
            foreach (Control ctl in containerCanvas.Children)
            {

                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(ListView))
                    ((ListView)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Button))
                    ((Button)ctl).Visibility = Visibility.Collapsed;
            }
        }
    }
}
