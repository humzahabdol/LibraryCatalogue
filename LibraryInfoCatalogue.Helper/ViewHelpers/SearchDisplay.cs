using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.Helper.BusinessClass
{
    public class SearchDisplay 
    {
        public string Available { get; set; }
        public string Display { get; set; }
        public DateTime CheckOutLogDate { get; set; }
        public Book Book { get; set; }
        public Person Person { get; set; }

       
    }
}
