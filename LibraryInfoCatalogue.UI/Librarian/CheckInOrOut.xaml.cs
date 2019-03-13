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
        private readonly BookRepository _bookRepository;
        private readonly CardholderRepository _cardholderRepository;
        private readonly BookHelper _bookHelper;
        private readonly LibrarianHelper _librarianHelper;
        private readonly ExceptionHelper _exceptionHelper;
        public CheckInOrOut()
        {
            InitializeComponent();
            _viewHelper = new ViewHelper();
            _checkoutLogRepository = new CheckoutLogRepository();
            _cardholderRepository = new CardholderRepository();
            _bookRepository = new BookRepository();
            _bookHelper = new BookHelper();
            _librarianHelper = new LibrarianHelper();
            _exceptionHelper = new ExceptionHelper();
            lbl_notFound.Visibility = Visibility.Hidden;
        }

        private void Btn_checkin_Click(object sender, RoutedEventArgs e)
        {
            lbl_notFound.Visibility = Visibility.Hidden;
            var checkoutlogs = _checkoutLogRepository.GetAll();
            var keywords = _bookHelper.SplitKeywords(txt_ISBN.Text);
            List<int> colIds = new List<int>();
            foreach (var kw in keywords)
            {
                foreach (var cl in checkoutlogs)
                {
                    try
                    {
                        if (cl.Book.ISBN.Equals(txt_ISBN.Text) && cl.Cardholder.LibraryCardID.Equals(txt_libraryCard.Text))
                        {
                            colIds.Add(cl.CheckOutLogID);
                        }
                    }
                    catch (Exception exception)
                    {
                        var t = _exceptionHelper.PrintAllInnerException(exception);
                        Console.WriteLine(t);
                        throw;
                    }
                   
                }
            }
            foreach (var colId in colIds)
            {
                _checkoutLogRepository.Delete(colId);
                SetLabel(false,"in");
                
            }
        }

        private void Btn_checkout_Click(object sender, RoutedEventArgs e)
        {
            lbl_notFound.Visibility = Visibility.Hidden;
            var checkoutlogs = _checkoutLogRepository.GetAll();

            var books = _bookRepository.GetAll();
            var cardholders = _cardholderRepository.GetAll();

            var book = books.Where(x => x.ISBN.Equals(txt_ISBN.Text)).FirstOrDefault();
            var cardholder = cardholders.Where(x => x.LibraryCardID.Equals(txt_libraryCard.Text)).FirstOrDefault();
            if (cardholder != null || book != null)
            {
                CheckOutLog checkOutLog = new CheckOutLog
                {
                    BookID = book.BookID,
                    CardholderID = cardholder.ID,
                    CheckOutDate = DateTime.Now
                };
                if (_librarianHelper.CanCheckOut(book, cardholder.ID))
                {
                    var checkedout = _checkoutLogRepository.Add(checkOutLog);
                    SetLabel(false, "out");
                }
                else
                {
                    SetLabel(true, "");
                }
            }
            else
            {
                SetLabel(true,". ISBN or Library Card is invalid.");
            }
            

        }

        private void SetLabel(bool cantCheckOut,string checkedOutOrIn)
        {
            if (cantCheckOut)
            {
                lbl_notFound.Content = "Cannot Check Out";
                lbl_notFound.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                lbl_notFound.Content = $"Successfuly checked {checkedOutOrIn}";
                lbl_notFound.Foreground = new SolidColorBrush(Colors.Green);
            }

            lbl_notFound.Visibility = Visibility.Visible;
        }
        
    }
}
