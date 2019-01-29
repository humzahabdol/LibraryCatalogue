using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalogue.Entities
{
    public class Book
    {
        public int BookID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int NumberOfCopies { get; set; }
        public int NumPages { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string YearPublished { get; set; }
        public string Language { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return $"ID: {BookID}, FN: {FirstName},LN: {LastName},P: {Publisher}, YP: {YearPublished}";
        }
        
    }
}
