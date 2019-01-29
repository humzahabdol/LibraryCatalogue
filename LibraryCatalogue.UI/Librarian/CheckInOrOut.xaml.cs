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
    /// Interaction logic for CheckInOrOut.xaml
    /// </summary>
    public partial class CheckInOrOut : Page
    {
        private readonly ISearchClass _searchClass;
        private readonly ILibrarianHelper _librarianHelper;
        private readonly ISqlDbRepository _sqlDbRepository;
        public CheckInOrOut(ISearchClass searchClass, ILibrarianHelper librarianHelper, ISqlDbRepository sqlDbRepository)
        {
            _searchClass = searchClass;
            _librarianHelper = librarianHelper;
            _sqlDbRepository = sqlDbRepository;
            InitializeComponent();
        }

        private void Btn_checkin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_checkout_Click(object sender, RoutedEventArgs e)
        {
            var isbnList = _searchClass.SplitKeywords(txt_isbn.Text);
            var books = _sqlDbRepository.GetBookByISBN(isbnList);
            var cardholder = _sqlDbRepository.GetPersonByLibraryCardId(txt_PatronNumber.Text);
            var isCheckoutable = _librarianHelper.IsBookCheckOutable(cardholder, books);

            if (isCheckoutable)
            {
                foreach (var book in books)
                {
                    var completed = _sqlDbRepository.CreateNewCheckoutLog(book.BookID, cardholder.ID);
                }
            }
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            frm_checkinorout.Content = new LibrarianMenu(_searchClass, _librarianHelper, _sqlDbRepository);
        }

        private void Hide()
        {
            lbl_patroncardnumber.Visibility = Visibility.Hidden;
            txt_PatronNumber.Visibility = Visibility.Hidden;
            lbl_isbn.Visibility = Visibility.Hidden;
            txt_isbn.Visibility = Visibility.Hidden;
            lbl_error.Visibility = Visibility.Hidden;
            btn_checkin.Visibility = Visibility.Hidden;
            btn_checkout.Visibility = Visibility.Hidden;
            btn_back.Visibility = Visibility.Hidden;
        }
    }
}
