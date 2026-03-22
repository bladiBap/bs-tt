using SolTestBackend.Core.Results;

namespace SolTestBackend.Domain.Users.Errors
{
    public static class PasswordError
    {
        public static Error IsEmpty =>
            Error.Validation(
                "Email.Empty",
                "Email cannot be empty."
            );

        public static Error FormatInvalid =>
            Error.Validation(
                "Email.FormatInvalid",
                "Email format is invalid."
            );
    }
}
