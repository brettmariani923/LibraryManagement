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


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class InsertGenresTest : DataTest
    {
        [Fact]
        public async Task InsertGenre_GivenCorrect_ShouldInsert_Successfully()
        {
            var request = new InsertGenres("Philosophy");

            var result = await _dataAccess.ExecuteAsync(request);

            Assert.True(result == 1);

            var deleteTask1 = _dataAccess.ExecuteAsync(new DeleteGenres(1));

            await Task.WhenAll(deleteTask1);
        }

        [Fact]
        public async Task InsertGenre_Given_GenreName_AlreadyExists_ShouldThrow_SqlException()
        {
            var genreName = "Fiction";

            var id = await _dataAccess.ExecuteAsync(new InsertGenres(genreName));

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertGenres(genreName)));

            var deleteTask1 = _dataAccess.ExecuteAsync(new DeleteGenres(1));

            await Task.WhenAll(deleteTask1);
        }

        [Theory]

        [InlineData(null!)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task InsertGenre_Given_NameIsInvalid_ShouldThrow_SqlException(string invalidName)
        {
            var request = new InsertGenres(invalidName);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(request));
        }

        [Fact]
        public async Task InsertGenre_WithTooLongName_ShouldThrowSqlException()
        {
            var longName = new string('A', MaxLength.GenreName +1);
            var request = new InsertGenres(longName);

            await Assert.ThrowsAsync<SqlException>(() => _dataAccess.ExecuteAsync(request));
        }

        [Fact]
        public async Task InsertGenre_Given_NameWithSpecialCharacters_ShouldInsert_Successfully()
        {
            var specialChar = "Science & Technology! ç ê ë è";

            var request = new InsertGenres(specialChar);

            var result = await _dataAccess.ExecuteAsync(request);

            Assert.True(result == 1);

            var deleteTask1 = _dataAccess.ExecuteAsync(new DeleteGenres(1));

            await Task.WhenAll(deleteTask1);
        }

        [Fact]
        public async Task InsertGenre_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.GenreName);

            var request = new InsertGenres(maxLength);

            var result = await _dataAccess.ExecuteAsync(request);

            Assert.True(result == 1);

            var deleteTask1 = _dataAccess.ExecuteAsync(new DeleteGenres(1));

            await Task.WhenAll(deleteTask1);
        }

        [Fact]
        public async Task InsertGenre_Given_ConcurrentInserts_ShouldInsert_Successfully()
        {
            var insert1Task = _dataAccess.ExecuteAsync(new InsertGenres("Action"));
            var insert2Task = _dataAccess.ExecuteAsync(new InsertGenres("Drama"));

            await Task.WhenAll(insert1Task, insert2Task);

            var genreID1 = insert1Task.Result;
            var genreID2 = insert2Task.Result;

            Assert.True(genreID1 > 0);
            Assert.True(genreID2 > 0);

            var deleteTask1 = _dataAccess.ExecuteAsync(new DeleteGenres(1));
            var deleteTask2 = _dataAccess.ExecuteAsync(new DeleteGenres(2));

            await Task.WhenAll(deleteTask1, deleteTask2);
        }

    }

}
