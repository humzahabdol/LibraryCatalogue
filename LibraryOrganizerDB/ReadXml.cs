using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LibraryOrganizerDB.Entities;

namespace LibraryOrganizerDB
{
    public class ReadXml
    {
        public static List<Book> GetBooksDataFromXmlDoc()
        {
            var bookXml =
                (from b in XDocument.Load(GetFullFileName("Books.xml")).Descendants("Book")
                 select b).ToList();
            List<Book> books = new List<Book>(bookXml.Count);


            foreach (var b in bookXml)
            {
                Book book = new Book();

                book.AuthorID = int.Parse(b.Element("AuthorID")?.Value);
                book.BookID = int.Parse(b.Element("BookID")?.Value);
                book.ISBN = b.Element("ISBN")?.Value;
                book.NumberOfCopies = int.Parse(b.Element("NumberOfCopies")?.Value);
                book.Title = b.Element("Title")?.Value;

                foreach (var x in b.Elements())
                {
                    switch (x.Name.ToString())
                    {
                        case "Subject":
                            book.Subject = b.Element("Subject").Value;
                            break;
                        case "YearPublished":
                            book.YearPublished = b.Element("YearPublished").Value;
                            break;
                        case "NumPages":
                            book.NumPages = int.Parse(b.Element("NumPages").Value);
                            break;
                        case "Description":
                            book.Description = b.Element("Description").Value;
                            break;
                        case "Publisher":
                            book.Publisher = b.Element("Publisher").Value;
                            break;
                        case "Language":
                            book.Language = b.Element("Language").Value;
                            break;
                    }
                }
                books.Add(book);
            }
            return books;
        }

        public static List<Librarian> GetLibrariansDataFromXmlDoc()
        {
            var librarianxml = (from l in XDocument.Load(GetFullFileName("Librarians.xml")).Descendants("Librarian") select l).ToList();
            List<Librarian> librarians = new List<Librarian>(librarianxml.Count);

            foreach (var l in librarianxml)
            {
                Librarian _librarian = new Librarian();

                _librarian.ID = int.Parse(l.Element("ID").Value);
                _librarian.Password = l.Element("Password").Value;
                _librarian.Phone = l.Element("Phone").Value;
                _librarian.UserID = l.Element("UserID").Value;

                librarians.Add(_librarian);

            }
            return librarians;
        }

        public static List<Author> GetAuthorsDataFromXmlDoc()
        {
            var authorxml =
                (from a in (XDocument.Load(GetFullFileName("Authors.xml")).Descendants("Author"))
                 select a).ToList();
            List<Author> authors = new List<Author>(authorxml.Count);
            foreach (var a in authorxml)
            {
                Author _author = new Author();
                _author.ID = int.Parse(a.Element("ID").Value);
                foreach (var x in a.Elements())
                {
                    switch (x.Name.ToString())
                    {
                        case "Bio":
                            _author.Bio = a.Element("Bio").Value;
                            break;
                    }
                }
                authors.Add(_author);

            }

            return authors;
        }

        public static List<Cardholder> GetCardHoldersFromXmlDox()
        {
            var cardHolderXML =
                (from c in (XDocument.Load(GetFullFileName("Cardholders.xml")).Descendants("Cardholder")) select c)
                .ToList();
            List<Cardholder> _cardHolders = new List<Cardholder>(cardHolderXML.Count);

            Cardholder _cardHolder;

            foreach (var c in cardHolderXML)
            {
                _cardHolder = new Cardholder();
                _cardHolder.LibraryCardID = c.Element("LibraryCardID").Value;
                _cardHolder.Phone = c.Element("Phone").Value;
                _cardHolder.ID = int.Parse(c.Element("ID").Value);
                _cardHolders.Add(_cardHolder);

            }

            return _cardHolders;
        }

        public static List<CheckOutLog> GetCheckOutLogsFromXmlDoc()
        {
            var checkOutLogXml = (from co in (XDocument.Load(GetFullFileName("CheckOutLog.xml")).Descendants("CheckOutLog")) select co)
                .ToList();
            List<CheckOutLog> checkOutLogs = new List<CheckOutLog>(checkOutLogXml.Count);

            foreach (var c in checkOutLogXml)
            {
                var _checkOutLog = new CheckOutLog();
                _checkOutLog.CardholderID = int.Parse(c.Element("CardholderID").Value);
                _checkOutLog.BookID = int.Parse(c.Element("BookID").Value);
                _checkOutLog.CheckOutDate = DateTime.Parse(c.Element("CheckOutDate").Value);
                checkOutLogs.Add(_checkOutLog);

            }

            return checkOutLogs;
        }

        public static List<Person> GetPeopleFromXmlDoc()
        {
            var peopleXML = (from p in (XDocument.Load(GetFullFileName("People.xml")).Descendants("Person")) select p).ToList();
            List<Person> people = new List<Person>(peopleXML.Count);
            foreach (var p in peopleXML)
            {
                var _people = new Person();
                _people.FirstName = p.Element("FirstName").Value;
                _people.LastName = p.Element("LastName").Value;
                _people.PersonID = int.Parse(p.Element("PersonID").Value);
                people.Add(_people);

            }
            return people;
        }
        internal static string GetFullFileName(string fileNameString)
        {
            DirectoryInfo rootDirectory;
            FileInfo[] files;

            try
            {
                rootDirectory =
                    new DirectoryInfo(@"..\..\..\");
            }
            catch
            {
                return null;
            }

            files =
                rootDirectory.GetFiles(fileNameString, SearchOption.AllDirectories);

            if (files.GetLength(0) == 0)
            {
                return null;
            }

            return files[0].FullName;
        }
    }
}
