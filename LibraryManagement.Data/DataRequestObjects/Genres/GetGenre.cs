using LibraryManagement.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataRequestObjects.Genres
{
    public class GetGenreByName : IDataFetch<string>
    {
        private readonly string _genreName;

        public GetGenreByName(string genreName)
        {
            _genreName = genreName;
        }

        public string GetSql()
        {
            return "SELECT Name FROM Genres WHERE Name = @GenreName";
        }

        public object GetParameters()
        {
            return new { GenreName = _genreName };
        }
    }

}
