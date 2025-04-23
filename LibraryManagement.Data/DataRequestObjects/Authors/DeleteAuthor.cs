using LibraryManagement.Data.Abstraction;

namespace LibraryManagement.Data.DataRequestObjects.Authors
{
    public class DeleteAuthor : IDataExecute
    {
        public int DeleteID { get; set; }

        public DeleteAuthor(int id)
        {
            DeleteID = id;
        }

        public object? GetParameters()
        {
            return this;
        }

        public string GetSql()
        {
            return "DELETE FROM Authors WHERE AuthorID = @AuthorID;";
        }
    
    }
}
