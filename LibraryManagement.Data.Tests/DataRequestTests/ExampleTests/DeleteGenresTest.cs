using LibraryManagement.Data.DataRequestObjects.Genres;
using LibraryManagement.Domain.Constants;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.DataRequestObjects.Examples;
using LibraryManagement.Data.Tests.Helpers;
using LibraryManagement.Data.Abstraction;
using System.ComponentModel.DataAnnotations;
using Azure.Core;

namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class DeleteGenresTest : DataTest
    {
        [Fact]
        public async Task DeleteGenre_GivenCorrectID_ShouldDelete_Successfully()
        {
            var genreName = "Romance";

            var result = await _dataAccess.ExecuteAsync(new InsertGenres(genreName));
            Assert.Equal(1, result);

            var genre = await _dataAccess.FetchAsync(new GetGenreByName(genreName));
            Assert.NotNull(genre);
            Assert.Equal(genreName, genre.Name);

            var deleteResult = await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));
            Assert.Equal(1, deleteResult);

            var checkDeleted = await _dataAccess.FetchAsync(new GetGenreByName(genreName));
            Assert.Null(checkDeleted);
        }

        [Fact]
        public async Task DeleteGenre_GivenIncorrectID_ShouldReturn_Zero()
        {
            var result = await _dataAccess.ExecuteAsync(new DeleteGenres(99999));
            Assert.Equal(0, result);
        }

    }
}
