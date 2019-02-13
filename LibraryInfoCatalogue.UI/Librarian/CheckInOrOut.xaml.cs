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
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.UI.Librarian
{
    /// <summary>
    /// Interaction logic for CheckInOrOut.xaml
    /// </summary>
    public partial class CheckInOrOut : Page
    {
        private readonly ViewHelper _viewHelper;
        private readonly CheckoutLogRepository _checkoutLogRepository;
        private readonly BookHelper _bookHelper;
        public CheckInOrOut()
        {
            InitializeComponent();
            _viewHelper = new ViewHelper();
            _checkoutLogRepository = new CheckoutLogRepository();
            _bookHelper = new BookHelper();
        }

        private void Btn_checkin_Click(object sender, RoutedEventArgs e)
        {
            var checkoutlogs = _checkoutLogRepository.GetAll();
            var keywords = _bookHelper.SplitKeywords(txt_ISBN.Text);
            foreach (var kw in keywords)
            {
                var id = checkoutlogs.Where(x => x.Cardholder.LibraryCardID ==  txt_libraryCard.Text  && x.Book.ISBN == kw).Select(x => x.CheckOutLogID).FirstOrDefault();
                _checkoutLogRepository.Delete(id);
            }
        }

        private void Btn_checkout_Click(object sender, RoutedEventArgs e)
        {
            CheckOutLog checkOutLog = new CheckOutLog();
            var checkoutlogs = _checkoutLogRepository.GetAll();
            
            
        }

        
    }
}
