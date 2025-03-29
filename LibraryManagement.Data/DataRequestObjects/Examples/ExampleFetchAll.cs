using LibraryManagement.Data.Abstraction;
using LibraryManagement.Data.DataTransferObjects;

namespace LibraryManagement.Data.DataRequestObjects.Examples
{
    public class ExampleFetchAll : IDataFetchList<ExampleFetch_DTO>
    {
        // Since this data request does not require parameters, we can return null.
        public object? GetParameters() => null;

        public string GetSql() =>
            @"
                -- We can pretend this selects many records from a table
                SELECT
                    1 [Id],
                    'This Is An Example' [Message],
                    GetUTCDate() [DateTimeUTC]
            ";
    }
}
