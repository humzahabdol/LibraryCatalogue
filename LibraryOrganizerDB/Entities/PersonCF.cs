using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB
{
    [Table("People")]
    public class PersonCF
    {
        [Key]
        public int  PersonID { get; set; }
        [Required, MaxLength(30),]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public virtual AuthorCF Author { get; set; }
        public virtual LibrarianCF Librarian { get; set; }
        public virtual CardholderCF Cardholder { get; set; }
    }
}
