using System.Data;

namespace LibraryManagement.Data.Abstraction
{
    /// <summary>
    /// This interface defines the factory that will return the IDbConnection used to connect to the database.
    /// </summary>
    public interface IDbConnectionFactory
    {
        public IDbConnection NewConnection();
    }
}
