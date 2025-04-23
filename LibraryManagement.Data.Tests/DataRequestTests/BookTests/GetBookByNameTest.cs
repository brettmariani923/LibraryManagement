using LibraryManagement.Data.DataRequestObjects.Books;
using LibraryManagement.Data.Tests.Helpers;
using LibraryManagement.Domain.Constants;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class GetBookByNameTest : DataTest
    {
        [Fact]
        public async Task GetBookByName_Given_ValidName_ShouldReturn_ExistingRecord()
        {
            var bookTitle = "Beyond Good and Evil";

            await _dataAccess.ExecuteAsync(new InsertBook(bookTitle, Variables.PublishedYear, Variables.Summary));
            
            var result = await _dataAccess.FetchAsync(new GetBookByName(bookTitle));

            await _dataAccess.ExecuteAsync(new DeleteBook(result.BookID));

            Assert.Equal(bookTitle, result.Title);
        }

        [Fact]
        public async Task GetBookByName_Given_InvalidName_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetBookByName("NonExistentBook"));
            Assert.Null(result);
        }

    }
}
