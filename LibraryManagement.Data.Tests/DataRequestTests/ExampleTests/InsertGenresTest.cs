using LibraryManagement.Data.DataRequestObjects.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.DataRequestObjects.Examples;
using LibraryManagement.Data.Tests.Helpers;
using LibraryManagement.Data.Abstraction;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Domain.Constants;
using Azure.Core;
using System.Runtime.InteropServices;
using LibraryManagement.Data.DataTransferObjects;

namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class InsertGenresTest : DataTest
    {
        [Fact]
        public async Task InsertGenre_GivenValidName_ShouldInsertSuccessfully()
        {
            var genreName = "Adventure";

            var insertResult = await _dataAccess.ExecuteAsync(new InsertGenres(genreName));
            Assert.Equal(1, insertResult);

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(genreName));
            Assert.NotNull(genre);
            Assert.Equal(genreName, genre.Name);

            await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));
        }

        [Fact]
        public async Task InsertGenre_Given_GenreName_AlreadyExists_ShouldThrow_SqlException()
        {
            var genreName = "Fiction";

            var result = await _dataAccess.ExecuteAsync(new InsertGenres(genreName));
            Assert.Equal(1, result);

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(genreName));
            Assert.NotNull(genre);

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
            Assert.Equal(1, result);

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(specialChar));
            Assert.NotNull(genre);
            Assert.Equal(specialChar, genre.Name);

            await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));
        }

        [Fact]
        public async Task InsertGenre_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.GenreName);

            var result = await _dataAccess.ExecuteAsync(new InsertGenres(maxLength));
            Assert.Equal(1, result);

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(maxLength));
            Assert.NotNull(genre);
            Assert.Equal(maxLength, genre.Name);

            await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));
        }

    }

}
