using LibraryManagement.Data.DataRequestObjects.Genres;
using LibraryManagement.Data.Tests.Helpers;

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

            var deleteResult = await _dataAccess.ExecuteAsync(new DeleteGenres(genre.GenreID));

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
