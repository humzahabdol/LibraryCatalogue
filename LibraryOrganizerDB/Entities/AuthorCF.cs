using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB
{
    [Table("Authors")]
    public class AuthorCF
    {
        [Key,ForeignKey("Person")]
        public int ID { get; set; }
        public string Bio { get; set; }
        

        public virtual PersonCF Person { get; set; }
        public virtual ICollection<BookCF> Books { get; set; }


    }
}
