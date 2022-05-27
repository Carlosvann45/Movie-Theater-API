namespace Movie.Theater.Enterprises.Utilities.ExceptionHandler
{
    /// <summary>
    /// Class to handle bad request throws
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }

}
