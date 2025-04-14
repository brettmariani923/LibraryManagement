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
            var AuthorName = "Plato";

            var insertResult = await _dataAccess.ExecuteAsync(new InsertAuthor(AuthorName));
            Assert.Equal(1, insertResult);

            var Author = await _dataAccess.FetchAsync(new GetAuthorByName(AuthorName));
            Assert.NotNull(Author);
            Assert.Equal(AuthorName, Author.Name);

            await _dataAccess.ExecuteAsync(new DeleteAuthor(Author.AuthorID));
        }

        [Fact]
        public async Task InsertAuthor_Given_AuthorName_AlreadyExists_ShouldThrow_SqlException()
        {
            var AuthorName = "Plotinus";

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(AuthorName));
            Assert.Equal(1, result);

            var Author = await _dataAccess.FetchAsync(new GetAuthorByName(AuthorName));
            Assert.NotNull(Author);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertAuthor(AuthorName)));

            await _dataAccess.ExecuteAsync(new DeleteAuthor(Author.AuthorID));
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
            await Assert.ThrowsAsync<SqlException>(() => _dataAccess.ExecuteAsync(new InsertAuthor(new string('A', MaxLength.AuthorName + 1))));
        }

        [Fact]
        public async Task InsertAuthor_Given_NameWithSpecialCharacters_ShouldInsert_Successfully()
        {
            var specialChar = "René Descartes";

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(specialChar));
            Assert.Equal(1, result);

            var Author = await _dataAccess.FetchAsync(new GetAuthorByName(specialChar));
            Assert.NotNull(Author);
            Assert.Equal(specialChar, Author.Name);

            await _dataAccess.ExecuteAsync(new DeleteAuthor(Author.AuthorID));
        }

        [Fact]
        public async Task InsertAuthor_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.AuthorName);

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(maxLength));
            Assert.Equal(1, result);

            var Author = await _dataAccess.FetchAsync(new GetAuthorByName(maxLength));
            Assert.NotNull(Author);
            Assert.Equal(maxLength, Author.Name);

            await _dataAccess.ExecuteAsync(new DeleteAuthor(Author.AuthorID));
        }

    }
}
