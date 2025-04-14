using LibraryManagement.Data.DataRequestObjects.Authors;
using LibraryManagement.Data.Tests.Helpers;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class GetAuthorByNameTest : DataTest
    {
        [Fact]
        public async Task GetAuthorByName_Given_ValidName_ShouldReturn_ExistingRecord()
        {
            var AuthorName = "Emannuel Swedenborg";

            await _dataAccess.ExecuteAsync(new InsertAuthor(AuthorName));
            var result = await _dataAccess.FetchAsync(new GetAuthorByName(AuthorName));

            Assert.NotNull(result);
            Assert.Equal(AuthorName, result.Name);

            await _dataAccess.ExecuteAsync(new DeleteAuthor(result.AuthorID));
        }

        [Fact]
        public async Task GetAuthorByName_Given_InvalidName_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetAuthorByName("NonExistentAuthor"));
            Assert.Null(result);
        }

    }
}
