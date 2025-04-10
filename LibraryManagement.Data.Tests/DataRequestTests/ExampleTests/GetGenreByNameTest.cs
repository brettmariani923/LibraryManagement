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
    public class GetGenreByNameTest : DataTest
    {
        [Fact]
        public async Task Genre_DTO_Given_ValidGenreId_ShouldReturn_ExistingRecord()
        {
            var insertGenres = new InsertGenres("History");
            var insert = await _dataAccess.ExecuteAsync(insertGenres);

            var request = new GetGenreByName("History");
            var result = await _dataAccess.FetchAsync(request);

            Assert.NotNull(result);
            Assert.Equal("History", result.Name);

            await _dataAccess.ExecuteAsync(new DeleteGenres(result.GenreID));

        }
    }
}
