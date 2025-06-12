using LibraryManagement.Data.DataRequestObjects.Genres;
using LibraryManagement.Data.Tests.Helpers;
using Microsoft.Data.SqlClient;
using LibraryManagement.Domain.Constants;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class InsertGenresTest : DataTest
    {
        [Fact]
        public async Task InsertGenre_GivenValidName_ShouldInsertSuccessfully()
        {
            var genreName = "Adventure";

            var insertResult = await _dataAccess.ExecuteAsync(new InsertGenres(genreName));

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(genreName));

            await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));

            Assert.Equal(1, insertResult);
        }

        [Fact]
        public async Task InsertGenre_Given_GenreName_AlreadyExists_ShouldThrow_SqlException()
        {
            var genreName = "Fiction";

            var result = await _dataAccess.ExecuteAsync(new InsertGenres(genreName));

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(genreName));

            await Assert.ThrowsAsync<SqlException>(async () =>await _dataAccess.ExecuteAsync(new InsertGenres(genreName)));

            await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));
        }

        [Theory]
        [InlineData(null!)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task InsertGenre_Given_NameIsInvalid_ShouldThrow_SqlException(string invalidName)
        {
            await Assert.ThrowsAsync<SqlException>(async () =>await _dataAccess.ExecuteAsync(new InsertGenres(invalidName)));
        }

        [Fact]
        public async Task InsertGenre_WithTooLongName_ShouldThrowSqlException()
        {
            await Assert.ThrowsAsync<SqlException>(() =>_dataAccess.ExecuteAsync(new InsertGenres(new string('A', MaxLength.GenreName + 1))));
        }

        [Fact]
        public async Task InsertGenre_Given_NameWithSpecialCharacters_ShouldInsert_Successfully()
        {
            var specialChar = "Science & Technology! ç ê ë è";

            var result = await _dataAccess.ExecuteAsync(new InsertGenres(specialChar));

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(specialChar));

            await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task InsertGenre_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.GenreName);

            var result = await _dataAccess.ExecuteAsync(new InsertGenres(maxLength));

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(maxLength));

            await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));

            Assert.Equal(1, result);
        }

    }

}
