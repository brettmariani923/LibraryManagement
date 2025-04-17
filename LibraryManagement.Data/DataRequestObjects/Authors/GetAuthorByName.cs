using LibraryManagement.Data.Abstraction;
using LibraryManagement.Data.DataTransferObjects;

namespace LibraryManagement.Data.DataRequestObjects.Authors
{
    public class GetAuthorByName : IDataFetch<Author_DTO>
    {
        public string Name { get; set; }
        public int AuthorID { get; set; }

        public GetAuthorByName(string name)
        {
            Name = name;
        }

        public object GetParameters()
        {
            return new { Name = Name };
        }

        public string GetSql()
        {
            return "SELECT AuthorID, Name FROM Authors WHERE Name = @Name";
        }
    }
}
