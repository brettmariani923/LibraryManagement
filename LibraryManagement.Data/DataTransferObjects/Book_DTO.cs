using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataTransferObjects
{
    public class Book_DTO
    {
        public string Title { get; set; }
        public int BookID { get; set; }
        public string PublishedYear { get; set; }
        public string Summary { get; set; }

    }
}
