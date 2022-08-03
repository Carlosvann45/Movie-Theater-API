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
        public const string EMAIL = "{email}";

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
        public const string LOG_GET_CUSTOMER_EMAIL = "Request recieved for GetCustomerByEmail.";

        // http error messages
        public const string SERVER_UNAVAILABLE_MESS = "There was a problem connecting to the database.";
        public const string CUSTOMER_EMAIL_CONFLICT = "The given User email already exist in database.";
        public const string CUSTOMER_EMAIL_NOTFOUND = "The username provided does not exist in the database.";
        public const string CUSTOMER_UNAUTHORIZED = "You are not authorized to access this account.";
        public const string BAD_TOKEN = "The token you have provided is not valid.";
        public const string EMAIL_MISMATCH = "The email from the token does not match the email provided in the path.";
        public const string NO_TOKEN = "This route requres a bearer token.";
        public const string NO_BEARER_TOKEN = "You must provide a bearer token. Example: Bearer token";

        // Misc
        public const string URL = "http://localhost:8085";

    }
}
