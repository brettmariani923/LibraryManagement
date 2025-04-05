using LibraryManagement.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataRequestObjects.Genres
{
    public class InsertGenres : IDataExecute
    {
        public string _Name { get; set; }

        public InsertGenres(string name)
        {
            _Name = name;
        }

        public object? GetParameters()
        {
            return new { Name = _Name };
        }

        public string GetSql()
        {
            return "INSERT INTO Genres (Name) VALUES (@Name);";
        }
    }
}
