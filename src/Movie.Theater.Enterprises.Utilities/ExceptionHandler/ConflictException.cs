namespace Movie.Theater.Enterprises.Utilities.ExceptionHandler
{
    /// <summary>
    /// class to handle conflict exceptions
    /// </summary>
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        {
        }
    }

}
