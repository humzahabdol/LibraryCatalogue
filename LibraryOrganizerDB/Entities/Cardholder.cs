using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB.Entities
{
    [Table("Cardholders")]
    public class Cardholder
    {
        [Key, ForeignKey("Person")]
        public int ID { get; set; }
        [Required, MaxLength(20)]
        public string Phone { get; set; }
        [Required, MaxLength(50)]
        public string LibraryCardID { get; set; }


        public virtual Person Person { get; set; }
        public virtual ICollection<CheckOutLog> CheckOutLogs { get; set; }


    }
}
