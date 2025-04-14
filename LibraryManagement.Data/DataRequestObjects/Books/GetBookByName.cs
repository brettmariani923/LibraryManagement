using LibraryManagement.Data.Abstraction;
using LibraryManagement.Data.DataTransferObjects;

namespace LibraryManagement.Data.DataRequestObjects.Books
{ 
    public class GetBookByName : IDataFetch<Book_DTO>
    {
        public string Name { get; set; }
        public int BookID { get; set; }

        public GetBookByName(string name)
        {
            Name = name;
        }

        public object GetParameters()
        {
            return new { Name = Name };
        }

        public string GetSql()
        {
            return "SELECT BookID, Name FROM Books WHERE Name = @Name";
        }

    }
}

