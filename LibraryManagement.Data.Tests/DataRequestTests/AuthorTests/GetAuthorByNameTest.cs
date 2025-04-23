using LibraryManagement.Data.DataRequestObjects.Authors;
using LibraryManagement.Data.Tests.Helpers;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class GetAuthorByNameTest : DataTest
    {
        [Fact]
        public async Task GetAuthorByName_Given_ValidName_ShouldReturn_ExistingRecord()
        {
            var authorName = "Emannuel Swedenborg";

            await _dataAccess.ExecuteAsync(new InsertAuthor(authorName));
            var result = await _dataAccess.FetchAsync(new GetAuthorByName(authorName));

            await _dataAccess.ExecuteAsync(new DeleteAuthor(result.AuthorID));

            Assert.Equal(authorName, result.Name);

        }

        [Fact]
        public async Task GetAuthorByName_Given_InvalidName_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetAuthorByName("NonExistentAuthor"));
            Assert.Null(result);
        }

    }
}
