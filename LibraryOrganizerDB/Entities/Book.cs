using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required, MaxLength(50)]
        public string ISBN { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, ForeignKey("Author")]
        public int AuthorID { get; set; }
        [Required]
        public int NumberOfCopies { get; set; }



        public int NumPages { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        [MaxLength(50)]
        public string Publisher { get; set; }
        public string YearPublished { get; set; }
        public string Language { get; set; }


        public virtual Author Author { get; set; }
        public virtual ICollection<CheckOutLog> CheckOutLogs { get; set; }

    }
}
