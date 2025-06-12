using LibraryManagement.Data.DataRequestObjects.Books;
using LibraryManagement.Data.Tests.Helpers;
using Microsoft.Data.SqlClient;
using LibraryManagement.Domain.Constants;


namespace LibraryManagement.Data.Tests.DataRequestTests.BookTests
{
    public class InsertBookTest : DataTest
    {

        [Fact]
        public async Task InsertBook_GivenValidName_ShouldInsertSuccessfully()
        {
            var bookTitle = "The Republic";

            var insertResult = await _dataAccess.ExecuteAsync(new InsertBook(bookTitle, Variables.PublishedYear, Variables.Summary));

            var book = await _dataAccess.FetchAsync(new GetBookByName(bookTitle));

            await _dataAccess.ExecuteAsync(new DeleteBook(book.BookID));

            Assert.Equal(1, insertResult);
        }

        [Fact]
        public async Task InsertBook_Given_bookTitle_AlreadyExists_ShouldThrow_SqlException()
        {
            var bookTitle = "Divine Love and Wisdom";

            var result = await _dataAccess.ExecuteAsync(new InsertBook(bookTitle, Variables.PublishedYear, Variables.Summary));

            var book = await _dataAccess.FetchAsync(new GetBookByName(bookTitle));

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertBook(bookTitle, Variables.PublishedYear, Variables.Summary)));

            await _dataAccess.ExecuteAsync(new DeleteBook(book.BookID));
        }

        [Theory]
        [InlineData(null!)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task InsertBook_Given_NameIsInvalid_ShouldThrow_SqlException(string invalidName)
        {
            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertBook(invalidName, Variables.PublishedYear, Variables.Summary)));
        }

        [Fact]
        public async Task InsertBook_WithTooLongName_ShouldThrowSqlException()
        {
            await Assert.ThrowsAsync<SqlException>(() => _dataAccess.ExecuteAsync(new InsertBook(new string('A', MaxLength.BookTitle + 1), Variables.PublishedYear, Variables.Summary)));
        }

        [Fact]
        public async Task InsertBook_Given_NameWithSpecialCharacters_ShouldInsert_Successfully()
        {
            var specialChar = "Tao Te Ching 道德经";
            var publishedYear = "600 BC";
            var summary = "A classic Chinese text attributed to Laozi, emphasizing harmony and balance in life.";

            var result = await _dataAccess.ExecuteAsync(new InsertBook(specialChar, publishedYear, summary));

            var book = await _dataAccess.FetchAsync(new GetBookByName(specialChar));

            await _dataAccess.ExecuteAsync(new DeleteBook(book.BookID));

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task InsertBook_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.BookTitle);
            var publishedYear = "2023";
            var summary = "A book with a title that is exactly at the maximum length limit.";

            var result = await _dataAccess.ExecuteAsync(new InsertBook(maxLength, publishedYear, summary));

            var book = await _dataAccess.FetchAsync(new GetBookByName(maxLength));

            await _dataAccess.ExecuteAsync(new DeleteBook(book.BookID));

            Assert.Equal(1, result);
        }

    }
}
