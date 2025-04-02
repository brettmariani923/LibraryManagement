using LibraryManagement.Data.Abstraction;
using LibraryManagement.Data.DataTransferObjects;

namespace LibraryManagement.Data.DataRequestObjects.Examples
{
    public class ExampleFetchById : IDataFetch<ExampleFetch_DTO>
    {
        #region Constructor

        public ExampleFetchById(int id) => Id = id;

        #endregion

        #region Public Properties / Parameters For SQL

        public int Id { get; set; }

        #endregion

        #region Public IDataRequest Methods

        // By returning 'this' we are returning the instance of this class. We will use the public properties ('Id' in this case) as the parameters.
        public object? GetParameters() => this;

        // Here we define the sql for this DataRequest.
        public string GetSql() =>
            @"
                SELECT
                    1 [Id],
                    'This Is An Example' [Message],
                    GetUTCDate() [DateTimeUTC]
            ";

        #endregion
    }
}
