using LibraryManagement.Data.DataRequestObjects.Books;
using LibraryManagement.Data.Tests.Helpers;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class GetBookByNameTest : DataTest
    {
        [Fact]
        public async Task GetBookByName_Given_ValidName_ShouldReturn_ExistingRecord()
        {
            var BookName = "Beyond Good and Evil";

            await _dataAccess.ExecuteAsync(new InsertBook(BookName));
            var result = await _dataAccess.FetchAsync(new GetBookByName(BookName));

            Assert.NotNull(result);
            Assert.Equal(BookName, result.Title);

            await _dataAccess.ExecuteAsync(new DeleteBook(result.BookID));
        }

        [Fact]
        public async Task GetBookByName_Given_InvalidName_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetBookByName("NonExistentBook"));
            Assert.Null(result);
        }

    }
}
