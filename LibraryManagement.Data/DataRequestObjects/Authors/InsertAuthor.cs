using LibraryManagement.Data.Abstraction;

namespace LibraryManagement.Data.DataRequestObjects.Authors
{
    public class InsertAuthor : IDataExecute
    {
        public string Name { get; set; }

        public InsertAuthor(string name)
        {
            Name = name;
        }

        public object? GetParameters()
        {
            return new { Name = Name };
        }

        public string GetSql()
        {
            return "INSERT INTO Authors (Name) VALUES (@Name);";
        }
    }
}
