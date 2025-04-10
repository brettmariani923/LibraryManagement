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
            var insertCommand = new InsertGenres(genreName);

            var insertResult = await _dataAccess.ExecuteAsync(insertCommand);

            Assert.Equal(1, insertResult);

            var getGenreQuery = new GetGenreByName(genreName);
            var genre = await _dataAccess.FetchAsync(getGenreQuery);

            Assert.NotNull(genre);
            Assert.Equal(genreName, genre.Name);

            var deleteCommand = new DeleteGenres(genre.GenreID);
            await _dataAccess.ExecuteAsync(deleteCommand);
        }



        [Fact]
        public async Task InsertGenre_Given_GenreName_AlreadyExists_ShouldThrow_SqlException()
        {
            var genreName = "Fiction";

            var request = new InsertGenres(genreName);
            var result = await _dataAccess.ExecuteAsync(request);
            Assert.Equal(1, result); 

            var getGenre = new GetGenreByName(genreName);
            var genre = await _dataAccess.FetchAsync(getGenre);
            Assert.NotNull(genre); 

            var request2 = new InsertGenres(genreName);
            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(request2));

            var deleteTask = new DeleteGenres(genre.GenreID);
            await _dataAccess.ExecuteAsync(deleteTask);
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

            var getGenre = new GetGenreByName(specialChar);

            var getResult = await _dataAccess.FetchAsync(getGenre);

            var deleteTask = new DeleteGenres(getResult.GenreID);

            await _dataAccess.ExecuteAsync(deleteTask);

        }

        [Fact]
        public async Task InsertGenre_Given_MaxLengthName_ShouldInsert_Successfully()
        {
            var maxLength = new string('A', MaxLength.GenreName);

            var request = new InsertGenres(maxLength);

            var result = await _dataAccess.ExecuteAsync(request);

            var getGenre = new GetGenreByName(request.Name);

            var getResult = await _dataAccess.FetchAsync(getGenre);

            Assert.NotNull(getResult);
            Assert.Equal(maxLength, getResult.Name);

            await _dataAccess.ExecuteAsync(new DeleteGenres(getResult.GenreID));

        }



    }

}
