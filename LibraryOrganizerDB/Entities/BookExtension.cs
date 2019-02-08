using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB.Entities
{
    public partial class Book
    {
        public string Available { get; set; }
        public override string ToString()
        {
            return
                $"Id: {BookID}, Title: {Title}, Author: {Author.Person.FirstName} {Author.Person.LastName}, Publisher: {Publisher}, Year Published: {YearPublished}";
        }
    }
}
