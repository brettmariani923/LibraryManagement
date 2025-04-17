using LibraryManagement.Data.Abstraction;

namespace LibraryManagement.Data.DataRequestObjects.Books
{
    internal class InsertBook : IDataExecute
    {
        public string Title { get; set; }
        public string PublishedYear { get; set; }
        public string Summary { get; set; }

        public InsertBook(string title, string publishedYear, string summary)
        {
            Title = title;
            PublishedYear = publishedYear;
            Summary = summary;
        }

        public object? GetParameters()
        {
            return this;
        }

        public string GetSql()
        {
            return "INSERT INTO Books (Title, PublishedYear, Summary) VALUES (@Title, @PublishedYear, @Summary);";
        }
    }
}
