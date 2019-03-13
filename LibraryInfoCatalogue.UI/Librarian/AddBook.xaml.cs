using System;
using System.Linq;
using System.Windows;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using LibraryOrganizerDB;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.UI.Librarian
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        private readonly AuthorRepository _authorRepository;
        private readonly PersonRepository _personRepository;
        private readonly BookRepository _bookRepository;
        private readonly CardholderRepository _cardholderRepository;
        public AddBook()
        {
            InitializeComponent();
            _authorRepository = new AuthorRepository();
            _personRepository = new PersonRepository();
            _bookRepository= new BookRepository();
            _cardholderRepository = new CardholderRepository();
            Initalize();
        }

        private void Initalize()
        {
            lbl_authorNotFound.Visibility = Visibility.Hidden;
            lbl_bio.Visibility = Visibility.Hidden;
            txtbx_bio.Visibility = Visibility.Hidden;
            lbl_bookExist.Visibility = Visibility.Hidden;
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            Initalize();
            var person = _personRepository.GetAll()
                .Where(x => x.FirstName == txtbx_firstName.Text && x.LastName == txtbx_lastName.Text).FirstOrDefault();
           
            if (person == null)
            {
                Person personObject = new Person
                {
                    FirstName = txtbx_firstName.Text,
                    LastName = txtbx_lastName.Text
                };
                _personRepository.Add(personObject);
                BioVisible();
                return;
            }
            else
            {
                var author = _authorRepository.GetAll().Where(x => x.Person.PersonID == person.PersonID).FirstOrDefault();
                if (author == null)
                {
                    BioVisible();
                    return;
                }
                else
                {
                    Author authorObejct = new Author();
                    if (author == null)
                    {
                        authorObejct.Bio = txtbx_bio.Text;
                        authorObejct.Person = person;                        
                        _authorRepository.Add(authorObejct);
                    }
                    //check if book already exist
                    Book book = new Book
                    {
                        AuthorID = authorObejct.ID,
                        ISBN = txtbx_isbn.Text,
                        NumberOfCopies = Convert.ToInt32(txtbx_numofcopies.Text),
                        Title = txtbx_title.Text,
                        NumPages = Convert.ToInt32(txtbx_numofpages?.Text),
                        Subject = txtbx_subject?.Text,
                        Description = txtbx_description?.Text,
                        Publisher = txtbx_publisher?.Text,
                        YearPublished = txtbx_yearpublished?.Text,
                        Language = txtbx_language?.Text
                    };

                    var foundBook = _bookRepository.GetAll().Where(x => x.Title.Equals(txtbx_title.Text, StringComparison.InvariantCultureIgnoreCase) 
                                                                        || x.ISBN.Equals(txtbx_isbn.Text, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (foundBook == null)
                    {
                        _bookRepository.Add(book);
                        this.Close();
                    }
                    else
                    {
                        lbl_bookExist.Visibility = Visibility.Visible;
                    }
                }
            }
        }


        private void BioVisible()
        {
            lbl_bio.Visibility = Visibility.Visible;
            lbl_authorNotFound.Visibility = Visibility.Visible;
            txtbx_bio.Visibility = Visibility.Visible;
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbx_firstName.Text))
            {
                this.Close();
            }
            else
            {
                var author = _authorRepository.GetAll().Where(x =>
                    x.Person.FirstName.Equals(txtbx_firstName.Text) && x.Person.LastName.Equals(txtbx_lastName.Text)).FirstOrDefault();
                if (author != null)
                {
                    var personId = author.Person.PersonID;
                    var book = _bookRepository.GetAll().Where(x => x.AuthorID == author.ID);
                    var cardholder = _cardholderRepository.GetAll()
                        .Where(x => x.Person.PersonID == personId).FirstOrDefault();
                    if (book == null)
                    {
                        _authorRepository.Delete(author.ID);
                    }

                    if (cardholder == null)
                    {
                        _personRepository.Delete(personId);
                    }
                }
            }

            this.Close();

        }
    }
}
