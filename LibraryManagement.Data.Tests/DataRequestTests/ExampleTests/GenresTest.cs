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


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class GenresTest : DataTest
    {
        [Fact]
        public async Task InsertGenre_GivenCorrect_ShouldInsert_Successfully()
        {
            var request = new InsertGenres("Philosophy");

            var result = await _dataAccess.ExecuteAsync(request);

            Assert.True(result == 1);

        }

        [Fact]
        public async Task InsertGenre_Given_GenreName_AlreadyExists_ShouldReturn_NoRowsUpdated()
        {
            var genreName = "Fiction";
            await _dataAccess.ExecuteAsync(new InsertGenres(genreName));

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertGenres(genreName));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task InsertGenre_Given_NameIsNull_ShouldThrow_SqlException()
        {
            var request = new InsertGenres(null!);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(request));
        }

        [Fact]
        public async Task InsertGenre_Given_NameIsEmpty_ShouldThrow_SqlException()
        {
            var request = new InsertGenres("");

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(request));
        }

        [Fact]
        public async Task InsertGenre_Given_NameIsWhitespace_ShouldThrow_SqlException()
        {
            var request = new InsertGenres("   ");
            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(request));
        }

        [Fact]
        public async Task InsertGenre_WithTooLongName_ShouldThrowSqlException()
        {
            var longName = new string('A', 300);
            var request = new InsertGenres(longName);

            await Assert.ThrowsAsync<SqlException>(() => _dataAccess.ExecuteAsync(request));
        }

        [Fact]
        public async Task InsertGenre_Given_NameWithSpecialCharacters_ShouldInsert_Successfully()
        {
            var specialChar = "Science & Technology!";
            var request = new InsertGenres(specialChar);

            var result = await _dataAccess.ExecuteAsync(request);

            Assert.True(result == 1);
        }

        [Fact]
        public async Task InsertGenre_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.GenreName);

            var request = new InsertGenres(maxLength);

            var result = await _dataAccess.ExecuteAsync(request);

            Assert.True(result == 1);
        }

        [Fact]
        public async Task InsertGenre_Given_ConcurrentInserts_ShouldInsert_Successfully()
        {
            var task1 = _dataAccess.ExecuteAsync(new InsertGenres("Action"));
            var task2 = _dataAccess.ExecuteAsync(new InsertGenres("Drama"));

            await Task.WhenAll(task1, task2);

            Assert.True(task1.Result == 1);
            Assert.True(task2.Result == 1);
        }

    }
}
