using LibraryManagement.Data.DataRequestObjects.Authors;
using LibraryManagement.Data.Tests.Helpers;
using Microsoft.Data.SqlClient;
using LibraryManagement.Domain.Constants;


namespace LibraryManagement.Data.Tests.DataRequestTests.AuthorTests
{
    public class InsertAuthorTest : DataTest
    {

        [Fact]
        public async Task InsertAuthor_GivenValidName_ShouldInsertSuccessfully()
        {
            var authorName = "Plato";

            var insertResult = await _dataAccess.ExecuteAsync(new InsertAuthor(authorName));
            Assert.Equal(1, insertResult);

            var author = await _dataAccess.FetchAsync(new GetAuthorByName(authorName));
            Assert.NotNull(author);
            Assert.Equal(authorName, author.Name);

            await _dataAccess.ExecuteAsync(new DeleteAuthor(author.AuthorID));
        }

        [Fact]
        public async Task InsertAuthor_Given_authorName_AlreadyExists_ShouldThrow_SqlException()
        {
            var authorName = "Plotinus";

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(authorName));
            Assert.Equal(1, result);

            var author = await _dataAccess.FetchAsync(new GetAuthorByName(authorName));
            Assert.NotNull(author);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertAuthor(authorName)));

            await _dataAccess.ExecuteAsync(new DeleteAuthor(author.AuthorID));
        }

        [Theory]
        [InlineData(null!)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task InsertAuthor_Given_NameIsInvalid_ShouldThrow_SqlException(string invalidName)
        {
            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertAuthor(invalidName)));
        }

        [Fact]
        public async Task InsertAuthor_WithTooLongName_ShouldThrowSqlException()
        {
            await Assert.ThrowsAsync<SqlException>(() => _dataAccess.ExecuteAsync(new InsertAuthor(new string('A', MaxLength.authorName + 1))));
        }

        [Fact]
        public async Task InsertAuthor_Given_NameWithSpecialCharacters_ShouldInsert_Successfully()
        {
            var specialChar = "René Descartes";

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(specialChar));
            Assert.Equal(1, result);

            var author = await _dataAccess.FetchAsync(new GetAuthorByName(specialChar));
            Assert.NotNull(author);
            Assert.Equal(specialChar, author.Name);

            await _dataAccess.ExecuteAsync(new DeleteAuthor(author.AuthorID));
        }

        [Fact]
        public async Task InsertAuthor_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.AuthorName);

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(maxLength));
            Assert.Equal(1, result);

            var author = await _dataAccess.FetchAsync(new GetAuthorByName(maxLength));
            Assert.NotNull(author);
            Assert.Equal(maxLength, author.Name);

            await _dataAccess.ExecuteAsync(new DeleteAuthor(author.AuthorID));
        }

    }
}
