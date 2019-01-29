using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB
{
    [Table("Librarians")]
    public class LibrarianCF
    {
        [Key, ForeignKey("Person")]
        public int ID { get; set; }
        [Required,MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string UserID { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }

        public virtual PersonCF Person { get; set; }
    }
}
