using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB.Entities
{
    [Table("CheckOutLog")]
    public class CheckOutLog
    {
        [Key]
        public int CheckOutLogID { get; set; }
        [Required, ForeignKey("Cardholder")]
        public int CardholderID { get; set; }
        [Required, ForeignKey("Book")]
        public int BookID { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual Cardholder Cardholder { get; set; }
    }
}
