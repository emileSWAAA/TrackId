namespace TrackId.Common.Constants
{
    public static class ErrorConstants
    {
        public static class Auth
        {
            public const string PasswordsDoNotMatch = "Passwords do not match.";
            public const string UnableToLogin = "Unable to login.";
            public const string UserAlreadyExists = "User already exists.";
            public const string CreatingAccountFailed = "Creating an account failed.";
            public const string UserDoesNotExist = "User does not exist.";
        }

        public static class Genre
        {
            public const string InvalidParameters = "Invalid parameters.";
            public const string NoGenresFound = "No genres found.";
        }

        public const string GeneralError = "Unknown error occurred.";
    }
}
