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
        public async Task DeleteGenre_GivenCorrect_ShouldDelete_Successfully()
        {
            var genreName = "Romance";

            var request = new InsertGenres(genreName);
            var result = await _dataAccess.ExecuteAsync(request);

            Assert.Equal(1, result);

            var getGenre = new GetGenreByName(genreName);
            var getResult = await _dataAccess.FetchAsync(getGenre);
                
            Assert.NotNull(getResult);
            Assert.Equal(genreName, getResult.Name);

            var deleteTask = new DeleteGenres(getResult.GenreID);
            var executeDeletion = await _dataAccess.ExecuteAsync(deleteTask);

            Assert.Equal(1, executeDeletion);

            var checkDeleted = await _dataAccess.FetchAsync(new GetGenreByName(genreName));
            Assert.Null(checkDeleted);
        }
    }
}
