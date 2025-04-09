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
            var genreName = "Philosophy";

            var request = new InsertGenres(genreName);

            var request2 = new DeleteGenres(1);

            var philosophy = new DataRequestObjects.Genres.Genre_DTO(1, genreName);

            var result = await _dataAccess.FetchAsync(philosophy);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteGenre_GivenConcurrent_ShouldDelete_Successfully()
        {

            var task1 = _dataAccess.ExecuteAsync(new InsertGenres("Action"));
            var task2 = _dataAccess.ExecuteAsync(new InsertGenres("Drama"));

            await Task.WhenAll(task1, task2);

            var deleteTask1 = _dataAccess.ExecuteAsync(new DeleteGenres(1));
            var deleteTask2 = _dataAccess.ExecuteAsync(new DeleteGenres(2));

            await Task.WhenAll(deleteTask1, deleteTask2);

            var action = new DataRequestObjects.Genres.Genre_DTO(1, "Action");
            var drama = new DataRequestObjects.Genres.Genre_DTO(2, "Drama");

            var result1 = await _dataAccess.FetchAsync(action);
            var result2 = await _dataAccess.FetchAsync(drama);

            Assert.Null(result1);
            Assert.Null(result2);
        }
    }
}
