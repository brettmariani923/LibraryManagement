using LibraryManagement.Data.Abstraction;


namespace LibraryManagement.Data.DataRequestObjects.Books
{
    public class DeleteBook : IDataExecute
    {
        public int BookID { get; set; }

        public DeleteBook(int id)
        {
            BookID = id;
        }

        public object? GetParameters()
        {
            return this;
        }

        public string GetSql()
        {
            return "DELETE FROM Books WHERE BookID = @BookID;";
        }

    }
}
