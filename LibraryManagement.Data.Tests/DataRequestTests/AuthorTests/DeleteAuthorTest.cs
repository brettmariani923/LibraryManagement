using LibraryManagement.Data.DataRequestObjects.Authors;
using LibraryManagement.Data.Tests.Helpers;


namespace LibraryManagement.Data.Tests.DataRequestTests.ExampleTests
{
    public class DeleteAuthorTest : DataTest
    {
        [Fact]
        public async Task DeleteAuthor_GivenCorrectID_ShouldDelete_Successfully()
        {
            var AuthorName = "Carl Jung";

            var result = await _dataAccess.ExecuteAsync(new InsertAuthor(AuthorName));
            Assert.Equal(1, result);

            var Author = await _dataAccess.FetchAsync(new GetAuthorByName(AuthorName));
            Assert.NotNull(Author);
            Assert.Equal(AuthorName, Author.Name);

            var deleteResult = await _dataAccess.ExecuteAsync(new DeleteAuthor(Author.AuthorID));
            Assert.Equal(1, deleteResult);

            var checkDeleted = await _dataAccess.FetchAsync(new GetAuthorByName(AuthorName));
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
