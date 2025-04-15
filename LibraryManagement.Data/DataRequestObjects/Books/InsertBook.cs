using LibraryManagement.Data.Abstraction;

namespace LibraryManagement.Data.DataRequestObjects.Books
{
    internal class InsertBook : IDataExecute
    {
        public string Title { get; set; }

        public InsertBook(string title)
        {
            Title = title;
        }

        public object? GetParameters()
        {
            return new { Title = Title };
        }

        public string GetSql()
        {
            return "INSERT INTO Books (Title) VALUES (@Title);";
        }
    }
}
