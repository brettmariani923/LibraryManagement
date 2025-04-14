using LibraryManagement.Data.DataRequestObjects.Genres;
using LibraryManagement.Data.Tests.Helpers;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class GetGenreByNameTest : DataTest
    {
        [Fact]
        public async Task GetGenreByName_Given_ValidName_ShouldReturn_ExistingRecord()
        {
            var genreName = "History";

            await _dataAccess.ExecuteAsync(new InsertGenres(genreName));
            var result = await _dataAccess.FetchAsync(new GetGenreByName(genreName));

            Assert.NotNull(result);
            Assert.Equal(genreName, result.Name);

            await _dataAccess.ExecuteAsync(new DeleteGenres(result.GenreID));
        }

        [Fact]
        public async Task GetGenreByName_Given_InvalidName_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetGenreByName("NonExistentGenre"));
            Assert.Null(result);
        }

    }
}
