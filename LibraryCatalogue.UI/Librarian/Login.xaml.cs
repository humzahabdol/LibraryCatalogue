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

namespace LibraryCatalogue.UI.Librarian
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private readonly ISearchClass _searchClass;
        private readonly ILibrarianHelper _librarianHelper;
        private readonly ISqlDbRepository _sqlDbRepository;
        public Login(ILibrarianHelper librarianHelper, ISearchClass searchClass, ISqlDbRepository sqlDbRepository)
        {
            _searchClass = searchClass;
            _librarianHelper = librarianHelper;
            _sqlDbRepository = sqlDbRepository;
            InitializeComponent();
        }

        private void Btn_signin_Click(object sender, RoutedEventArgs e)
        {

            bool isSignin = _librarianHelper.IsUserLogin(txt_username.Text, txt_password.Password);
            if (isSignin)
            {
                LibrarianMenu librarianMenu = new LibrarianMenu(_searchClass, _librarianHelper,_sqlDbRepository);
                this.NavigationService.Navigate(librarianMenu);

            }
            else
            {
                lbl_loginfailed.Content = "Login failed. Username or Password does not exist";
                lbl_loginfailed.Visibility = Visibility.Visible;
            }
            
        }
    }
}
