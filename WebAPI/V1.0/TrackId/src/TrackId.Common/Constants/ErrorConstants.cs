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

        public const string GeneralError = "Unknown error occurred.";
    }
}
