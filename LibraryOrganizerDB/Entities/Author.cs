using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOrganizerDB.Entities;

namespace LibraryOrganizerDB
{
    [Table("Authors")]
    public class Author
    {
        [Key, ForeignKey("Person")]
        public int ID { get; set; }
        public string Bio { get; set; }


        public virtual Person Person { get; set; }
        public virtual ICollection<Book> Books { get; set; }


    }
}
