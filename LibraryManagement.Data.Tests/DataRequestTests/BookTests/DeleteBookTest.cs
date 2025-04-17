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

            var Book = await _dataAccess.FetchAsync(new GetBookByName(bookTitle));
            Assert.NotNull(Book);
            Assert.Equal(bookTitle, Book.Title);

            var deleteResult = await _dataAccess.ExecuteAsync(new DeleteBook(Book.BookID));
            Assert.Equal(1, deleteResult);

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
