using LibraryManagement.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataRequestObjects.Genres
{
    public class Genre_DTO : IDataFetch<Genre_DTO>
    {
        public int GenreID { get; set; }

        public Genre_DTO(int genreId)
        {
            GenreID = genreId;
        }

        public object GetParameters()
        {
            return new { GenreID = GenreID };
        }

        public string GetSql()
        {
            return "SELECT GenreID, Name FROM Genres WHERE GenreID = @GenreID";
        }

    }
}
