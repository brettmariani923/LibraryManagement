using LibraryManagement.Data.DataRequestObjects.Examples;
using LibraryManagement.Data.Tests.Helpers;

namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class ExampleFetchByIdTests : DataTest
    {
        // This is just an example of using the DataTest base class to get a connection to the database for executing requests.
        // With our implementation, each 'DataTest' would be integrating with an actual SqlServer database. We may have to keep that in mind when multiple tests are running at the same time.
        // For example, if one test delets all records and another test expects records to exist, one test could cause the other to fail if both run at the same time.
        [Fact]
        public async Task ExampleFetchById_Given_RecordExists_ShouldReturn_ExistingRecord()
        {
            // Here we initialize the DataRequest that we are going to test.
            // Sometimes we might have to 'setup' data in the database before we can execute our request.
            // For example, we might need to insert a record before we try to fetch it.
            var request = new ExampleFetchById(5);

            // Here we send the request to the database and store the result in a variable so we can assert that we got the expected results.
            var result = await _dataAccess.FetchAsync(request);

            // Here we are just asserting that the result is not null. Typically you would have more assertions, such as making sure your object has the expected values.
            Assert.NotNull(result);
        }

        // This class is just for an example, but ideally you would have multiple tests.
        // You want to always consider the different possibilities/outcomes for the method/class you're testing, and then
        // ensure that you have tests to cover for all scenarios. For example you might have one test to
        // assert you get the record when it exists, but you might have another test that you get null when no record exists.
    }
}
