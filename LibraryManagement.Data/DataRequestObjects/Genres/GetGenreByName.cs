using LibraryManagement.Data.Abstraction;
using LibraryManagement.Data.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataRequestObjects.Genres
{
    public class GetGenreByName : IDataFetch<Genre_DTO>
    {
        public string Name { get; set; }

        public GetGenreByName(string name)
        {
            Name = name;
        }

        public object GetParameters()
        {
            return this;
        }

        public string GetSql()
        {
            return "SELECT GenreID, Name FROM Genres WHERE Name = @Name";
        }

    }
}
