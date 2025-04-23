using LibraryManagement.Data.Abstraction;


namespace LibraryManagement.Data.DataRequestObjects.Genres
{
    public class DeleteGenres : IDataExecute
    {
        public int GenreID { get; set; }

        public DeleteGenres(int id)
        {
            GenreID = id;
        }

        public object? GetParameters()
        {
            return this;
        }

        public string GetSql()
        {
            return "DELETE FROM Genres WHERE GenreID = @GenreID;";
        }

    }
}
