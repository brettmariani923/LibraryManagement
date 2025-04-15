using LibraryManagement.Data.Abstraction;
using LibraryManagement.Data.DataTransferObjects;

namespace LibraryManagement.Data.DataRequestObjects.Books
{ 
    public class GetBookByName : IDataFetch<Library_DTO>
    {
        public string Title { get; set; }
        public int BookID { get; set; }

        public GetBookByName(string title)
        {
            Title = title;
        }

        public object GetParameters()
        {
            return new { Title = Title };
        }

        public string GetSql()
        {
            return "SELECT BookID, Title, PublishedYear, Summary FROM Books WHERE Title = @Title";
        }

    }
}

