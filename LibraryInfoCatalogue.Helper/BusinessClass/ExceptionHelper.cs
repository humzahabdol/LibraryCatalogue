using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoCatalogue.Helper.BusinessClass
{
    public class ExceptionHelper
    {
        public string PrintAllInnerException(Exception e)
        {

            if (null != e.InnerException)
            {
                PrintAllInnerException(e);
            }

            return e.Message;
        }

    }
}
