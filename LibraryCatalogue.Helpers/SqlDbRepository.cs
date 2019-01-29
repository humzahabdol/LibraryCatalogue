﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCatalogue.Entities;
using LibraryOrganizerDB;
using Search;

namespace LibraryCatalogue.Helpers
{
    public class SqlDbRepository : ISqlDbRepository
    {
        private readonly ISearchClass _searchClass;
        protected readonly LibraryContext _context;

        public SqlDbRepository()
        {
            _context = new LibraryContext();
            _searchClass = new SearchClass();
        }

        #region Get List
        public List<Librarian> GetLibrarians()
        {
            var libcf = _context.Librarians.ToList();
            return ConvertLibCftoLibrarians(libcf);
        }

        public List<Person> GetPeople()
        {
            var personCf = _context.People.ToList();
            return ConvertPersoncftoPerson(personCf);
        }
        public List<Author> GetAuthors()
        {
            var authorcf = _context.Authors.ToList();
            return ConvertAuthorCFtoAuthors(authorcf);
        }
        public List<Cardholder> GetCardholders()
        {
            var cardholderCfs = _context.Cardholders.ToList();
            return ConvertCardholderCFtoCardholders(cardholderCfs);
        }
        public List<CheckOutLog> GetCheckOutLogs()
        {
            var checkoutlogCFs = _context.CheckOutLogs.ToList();
            return ConvertCheckoutLogCdtoCheckOutLogs(checkoutlogCFs);
        }
        public List<Book> GetBooks()
        {
            var bookCFs = _context.Books.ToList();
            return ConvertBookCFToBooks(bookCFs);
        }
        #endregion

        

        private List<Librarian> ConvertLibCftoLibrarians(List<LibrarianCF> libcf)
        {
            List<Librarian> librarians = new List<Librarian>(libcf.Count);
            foreach (LibrarianCF lcf in libcf)
            {
                Librarian l = new Librarian
                {
                    ID = lcf.ID,
                    UserID = lcf.UserID,
                    Password = lcf.Password,
                    Phone = lcf.Password
                };
                librarians.Add(l);
            }

            return librarians;
        }

        private List<Person> ConvertPersoncftoPerson(List<PersonCF> personCf)
        {
            List<Person> people = new List<Person>(personCf.Count);
            foreach (var cf in personCf)
            {
                Person person = new Person
                {
                    FirstName = cf.FirstName,
                    LastName = cf.LastName,
                    PersonID = cf.PersonID
                };
                people.Add(person);
            }

            return people;
        }

        private List<Author> ConvertAuthorCFtoAuthors(List<AuthorCF> authorcf)
        {
            List<Author> authors = new List<Author>(authorcf.Count);
            foreach (var cf in authorcf)
            {
                Author author = new Author
                {
                    ID = cf.ID,
                    Bio = cf.Bio
                };
                authors.Add(author);
            }

            return authors;
        }

        private List<Cardholder> ConvertCardholderCFtoCardholders(List<CardholderCF> cardholderCfs)
        {
            List<Cardholder> cardholders = new List<Cardholder>(cardholderCfs.Count);
            foreach (var cf in cardholderCfs)
            {
                Cardholder cardholder = new Cardholder
                {
                    ID = cf.ID,
                    LibraryCardID = cf.LibraryCardID,
                    Phone = cf.Phone
                };
                cardholders.Add(cardholder);
            }

            return cardholders;
        }

        private List<CheckOutLog> ConvertCheckoutLogCdtoCheckOutLogs(List<CheckOutLogCF> checkOutLogCfs)
        {
            List<CheckOutLog> checkOutLogs = new List<CheckOutLog>(checkOutLogCfs.Count);
            foreach (var cf in checkOutLogCfs)
            {
                CheckOutLog checkOutLog = new CheckOutLog
                {
                    BookID = cf.BookID,
                    CardholderID = cf.CardholderID,
                    CheckOutDate = cf.CheckOutDate,
                    CheckOutLogID = cf.CheckOutLogID
                };
                checkOutLogs.Add(checkOutLog);
            }

            return checkOutLogs;
        }

        private List<Book> ConvertBookCFToBooks(List<BookCF> bookCfs)
        {

            List<Book> books = new List<Book>(bookCfs.Count);

            foreach (var cf in bookCfs)
            {
                Book book = new Book
                {
                    BookID = cf.BookID,
                    AuthorID = cf.AuthorID,
                    Description = cf.Description,
                    ISBN = cf.ISBN,
                    Language = cf.Language,
                    NumberOfCopies = cf.NumberOfCopies,
                    NumPages = cf.NumPages,
                    Publisher = cf.Publisher,
                    Subject = cf.Subject,
                    Title = cf.Title,
                    YearPublished = cf.YearPublished
                };

                // set first and  last name
                var person = GetPersonById(book.AuthorID);
                book.FirstName = person.FirstName;
                book.LastName = person.LastName;

                //check if is available
                book.IsAvailable = IsBookAvailable(book);

                books.Add(book);
            }

            return books;
        }
        public Author GetAuthorById(int id)
        {
            var authorcf = _context.Authors.ToList();
            var authors = ConvertAuthorCFtoAuthors(authorcf);
            foreach (var author in authors)
            {
                if (author.ID == id)
                {
                    return author;
                }
            }
            return null;

        }

        private bool IsBookAvailable(Book book)
        {
            var checkouLogcf = _context.CheckOutLogs.ToList();
            var checkoutlogList = ConvertCheckoutLogCdtoCheckOutLogs(checkouLogcf);
            var foundBookInCheckOutLog = checkoutlogList.Where(x => x.BookID == book.BookID).Count();
            if (foundBookInCheckOutLog < book.NumberOfCopies)
            {
                return true;
            }

            return false;
        }
        public Person GetPersonById(int id)
        {
            var peopleCF = _context.People.ToList();
            var people = ConvertPersoncftoPerson(peopleCF);
            foreach (var person in people)
            {
                if (person.PersonID == id)
                {
                    return person;
                }
            }
            return null;
        }
    }
}
