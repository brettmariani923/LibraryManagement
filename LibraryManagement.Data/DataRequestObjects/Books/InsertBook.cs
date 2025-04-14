using LibraryManagement.Data.Abstraction;

namespace LibraryManagement.Data.DataRequestObjects.Books
{
    internal class InsertBook : IDataExecute
    {
        public string Name { get; set; }

        public InsertBook(string name)
        {
            Name = name;
        }

        public object? GetParameters()
        {
            return new { Name = Name };
        }

        public string GetSql()
        {
            return "INSERT INTO Books (Name) VALUES (@Name);";
        }
    }
}
