namespace Movie.Theater.Enterprises.Utilities.ExceptionHandler
{
    /// <summary>
    /// class to represent an error response
    /// </summary>
    public class Error
    {
        public string? Timestamp { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ErrorCode { get; set; }
    }

}
