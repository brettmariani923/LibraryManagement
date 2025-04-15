using LibraryManagement.Data.Abstraction;
using LibraryManagement.Data.Implementation;

namespace LibraryManagement.Data.Tests.Helpers
{
    public abstract class DataTest
    {
        protected readonly IDataAccess _dataAccess;

        //Adding this because for now because hidden isn't showing up in my newest branch, we probably just need to update git ignore on the main branch.
       

        public DataTest()
        {
            /* 
                Hidden class is added to git ignore. You may need to create the class called 'Hidden' in this same namespace/folder.
                Ensure that your class has a constant named 'ConnectionString' which will be used to connect to the Database you use for running unit tests.

                Example:
                        */

                    
            var connectionFactory = new SqlConnectionFactory(Hidden.ConnectionString);

            _dataAccess = new DataAccess(connectionFactory);
        }
    }
}
