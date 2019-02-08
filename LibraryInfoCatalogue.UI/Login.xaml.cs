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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private readonly LibrarianHelper _librarianHelper;
        public Login()
        {
            InitializeComponent();
            _librarianHelper = new LibrarianHelper();
            lbl_error.Visibility = Visibility.Hidden;
        }

        private void Btn_signin_Click(object sender, RoutedEventArgs e)
        {
            if (_librarianHelper.IsUserLogin(txt_username.Text, txt_passwordBox.Password))
            {
                lbl_error.Visibility = Visibility.Hidden;
                LibraryMenu libraryMenu = new LibraryMenu();
                frm_login.Content = libraryMenu;
                Hide();
            }
            else
            {
                lbl_error.Visibility = Visibility.Visible;
            }
        }

        private void Hide()
        {
            foreach (Control ctl in containerCanvas.Children)
            {

                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Collapsed;
                if (ctl is PasswordBox box)
                    box.Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Button))
                    ((Button)ctl).Visibility = Visibility.Collapsed;
            }
        }
    }
}
