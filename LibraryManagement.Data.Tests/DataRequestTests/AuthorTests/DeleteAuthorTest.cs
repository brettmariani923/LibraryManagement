using LibraryManagement.Data.DataRequestObjects.Authors;
using LibraryManagement.Data.Tests.Helpers;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class DeleteAuthorTest : DataTest
    {
        [Fact]
        public async Task DeleteAuthor_GivenCorrectID_ShouldDelete_Successfully()
        {
            var authorName = "Carl Jung";

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(authorName));
            Assert.Equal(1, result);

            var author = await _dataAccess.FetchAsync(new GetAuthorByName(authorName));

            var deleteResult = await _dataAccess.ExecuteAsync(new DeleteAuthor(author.AuthorID));

            var checkDeleted = await _dataAccess.FetchAsync(new GetAuthorByName(authorName));
            Assert.Null(checkDeleted);
        }

        [Fact]
        public async Task DeleteAuthor_GivenIncorrectID_ShouldReturn_Zero()
        {
            var result = await _dataAccess.ExecuteAsync(new DeleteAuthor(99999));
            Assert.Equal(0, result);
        }

    }
}
