namespace Movie.Theater.Enterprises
{
    /// <summary>
    /// Constants class to represent constant strings in the application
    /// </summary>
    public static class Constants
    {
        // Application format type
        public const string APP_CONSUMES = "application/json";
        public const string APP_PRODUCES = "application/json";

        // Endpoints
        public const string CUSTOMER_ENDPOINT = "/customers";
        public const string REGISTER_ENDPOINT = "register";
        public const string LOGIN_ENDPOINT = "login";
        public const string REFRESH_ENDPOINT = "refresh/token";

        // http error codes
        public const string INTERAL_SERVER_ERROR = "500 Server Error";
        public const string SERVER_UNAVAILABLE_ERROR = "503 Service Unavailable";
        public const string BAD_REQUEST_ERROR = "400 Bad Request";
        public const string NOT_FOUND_ERROR = "404 Not Found";
        public const string CONFLICT_ERROR = "409 Conflict";
        public const string UNAUTHORIZED_ERROR = "401 Unauthorized";

        // log messages
        public const string LOG_REGISTER_CUSTOMER = "Request recieved for RegisterCustomerAsync.";
        public const string LOG_LOG_IN_CUSTOMER = "Request recieved for LoginInCustomerAsync.";
        public const string LOG_REFRESH_CUSTOMER_TOKEN = "Request recieved for RefreshCustomerToken.";

        // http error messages
        public const string SERVER_UNAVAILABLE_MESS = "There was a problem connecting to the database.";
        public const string CUSTOMER_EMAIL_CONFLICT = "The given User email already exist in database.";
        public const string CUSTOMER_EMAIL_NOTFOUND = "The username provided does not exist in the database.";
        public const string CUSTOMER_UNAUTHORIZED = "You are not authorized to access this account.";
        public const string BAD_TOKEN = "The token you have provided is not valid.";

        // Misc
        public const string URL = "http://localhost:8085";

    }
}
