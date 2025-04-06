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
        public string _DeleteName { get; set; }

        public DeleteGenres(string name)
        {
            _DeleteName = name;
        }

        public object? GetParameters()
        {
            return new { Name = _DeleteName };
        }

        public string GetSql()
        {
            return "DELETE FROM Genres WHERE Name = @Name;";
        }
   
    }
}
