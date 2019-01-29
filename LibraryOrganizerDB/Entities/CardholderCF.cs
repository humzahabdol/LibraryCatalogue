using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB
{
    [Table("Cardholders")]
    public class CardholderCF
    {
        [Key, ForeignKey("Person")]
        public int ID { get; set; }
        [Required,MaxLength(20)]
        public string Phone { get; set; }
        [Required,MaxLength(50)]
        public string LibraryCardID { get; set; }


        public virtual PersonCF Person { get; set; }
        public virtual ICollection<CheckOutLogCF> CheckOutLogs { get; set; }


    }
}
