using LibraryManagement.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataRequestObjects.Genres
{
    public class DeleteGenres : IDataExecute
    {
        public int DeleteID { get; set; }

        public DeleteGenres(int id)
        {
            DeleteID = id;
        }

        public object? GetParameters()
        {
            return new { GenreID = DeleteID };
        }

        public string GetSql()
        {
            return "DELETE FROM Genres WHERE GenreID = @GenreID;";
        }
   
    }
}
