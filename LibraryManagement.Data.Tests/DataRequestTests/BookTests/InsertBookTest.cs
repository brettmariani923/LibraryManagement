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
            var BookName = "The Republic";

            var insertResult = await _dataAccess.ExecuteAsync(new InsertBook(BookName));
            Assert.Equal(1, insertResult);

            var Book = await _dataAccess.FetchAsync(new GetBookByName(BookName));
            Assert.NotNull(Book);
            Assert.Equal(BookName, Book.Title);

            await _dataAccess.ExecuteAsync(new DeleteBook(Book.BookID));
        }

        [Fact]
        public async Task InsertBook_Given_BookName_AlreadyExists_ShouldThrow_SqlException()
        {
            var BookName = "Divine Love and Wisdom";

            var result = await _dataAccess.ExecuteAsync(new InsertBook(BookName));
            Assert.Equal(1, result);

            var Book = await _dataAccess.FetchAsync(new GetBookByName(BookName));
            Assert.NotNull(Book);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertBook(BookName)));

            await _dataAccess.ExecuteAsync(new DeleteBook(Book.BookID));
        }

        [Theory]
        [InlineData(null!)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task InsertBook_Given_NameIsInvalid_ShouldThrow_SqlException(string invalidName)
        {
            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertBook(invalidName)));
        }

        [Fact]
        public async Task InsertBook_WithTooLongName_ShouldThrowSqlException()
        {
            await Assert.ThrowsAsync<SqlException>(() => _dataAccess.ExecuteAsync(new InsertBook(new string('A', MaxLength.BookName + 1))));
        }

        [Fact]
        public async Task InsertBook_Given_NameWithSpecialCharacters_ShouldInsert_Successfully()
        {
            var specialChar = "Tao Te Ching 道德经";

            var result = await _dataAccess.ExecuteAsync(new InsertBook(specialChar));
            Assert.Equal(1, result);

            var Book = await _dataAccess.FetchAsync(new GetBookByName(specialChar));
            Assert.NotNull(Book);
            Assert.Equal(specialChar, Book.Title);

            await _dataAccess.ExecuteAsync(new DeleteBook(Book.BookID));
        }

        [Fact]
        public async Task InsertBook_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.BookName);

            var result = await _dataAccess.ExecuteAsync(new InsertBook(maxLength));
            Assert.Equal(1, result);

            var Book = await _dataAccess.FetchAsync(new GetBookByName(maxLength));
            Assert.NotNull(Book);
            Assert.Equal(maxLength, Book.Title);

            await _dataAccess.ExecuteAsync(new DeleteBook(Book.BookID));
        }

    }
}
