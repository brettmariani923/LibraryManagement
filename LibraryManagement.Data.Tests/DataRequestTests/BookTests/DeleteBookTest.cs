using LibraryManagement.Data.DataRequestObjects.Books;
using LibraryManagement.Data.Tests.Helpers;
using LibraryManagement.Domain.Constants;

namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class DeleteBooksTest : DataTest
    {
        [Fact]
        public async Task DeleteBook_GivenCorrectID_ShouldDelete_Successfully()
        {
            var bookTitle = "Aion";

            var result = await _dataAccess.ExecuteAsync(new InsertBook(bookTitle, Variables.PublishedYear, Variables.Summary));
            Assert.Equal(1, result);

            var book = await _dataAccess.FetchAsync(new GetBookByName(bookTitle));

            var deleteResult = await _dataAccess.ExecuteAsync(new DeleteBook(book.BookID));

            var checkDeleted = await _dataAccess.FetchAsync(new GetBookByName(bookTitle));
            Assert.Null(checkDeleted);
        }

        [Fact]
        public async Task DeleteBook_GivenIncorrectID_ShouldReturn_Zero()
        {
            var result = await _dataAccess.ExecuteAsync(new DeleteBook(99999));
            Assert.Equal(0, result);
        }

    }
}
