namespace LibraryManagement.Data.DataTransferObjects
{
    /// <summary>
    /// This is an example 'DTO' class. A 'Data Transfer Object' is typically a 'POCO' (Plain Old Class Object) that just has properties which represent the fields/data being transferred.
    /// In this case, we are using 'Data Transfer Objects' to represent the objects that are being returned from our database.
    /// </summary>
    public class ExampleFetch_DTO
    {
        public int Id { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime DateTimeUTC { get; set; }
    }
}
