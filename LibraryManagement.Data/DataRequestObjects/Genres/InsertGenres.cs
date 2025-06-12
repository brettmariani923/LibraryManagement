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
        public string Name { get; set; }

        public InsertGenres(string name)
        {
            Name = name;
        }

        public object? GetParameters()
        {
            return this;
        }

        public string GetSql()
        {
            return "INSERT INTO Genres (Name) VALUES (@Name);";
        }
    }
}
