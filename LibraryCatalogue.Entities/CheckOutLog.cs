using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalogue.Entities
{
    public class CheckOutLog
    {
        public int CheckOutLogID { get; set; }
        public int CardholderID { get; set; }
        public int BookID { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
