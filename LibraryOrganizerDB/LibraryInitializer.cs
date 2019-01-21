using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB
{
    public class LibraryInitializer : DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            // add people to database
            var people = ReadXml.GetPeopleFromXmlDoc();
            people.ForEach(p => context.People.Add(p));
            context.SaveChanges();

            // add librarians to database
            var librarians = ReadXml.GetLibrariansDataFromXmlDoc();
            librarians.ForEach(l => context.Librarians.Add(l));
            context.SaveChanges();

            // add Authors to database
            var authors = ReadXml.GetAuthorsDataFromXmlDoc();
            authors.ForEach(a => context.Authors.Add(a));
            context.SaveChanges();

            // add Cardholders to database
            var cardHolders = ReadXml.GetCardHoldersFromXmlDox();
            cardHolders.ForEach(cH => context.Cardholders.Add(cH));
            context.SaveChanges();

            // add books to database
            var books = ReadXml.GetBooksDataFromXmlDoc();
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();

            // add Check out logs to database   
            var checkOutLogs = ReadXml.GetCheckOutLogsFromXmlDoc();
            checkOutLogs.ForEach(col => context.CheckOutLogs.Add(col));
            context.SaveChanges();
        }
    }
}
