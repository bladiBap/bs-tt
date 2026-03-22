using SolTestBackend.Core.Results;

namespace SolTestBackend.Domain.Users.Errors
{
    public static class EmailError
    {
        public static Error IsEmpty =>
            Error.Validation(
                "Email.Empty",
                "Email cannot be empt1y."
            );
        
        public static Error FormatInvalid =>
            Error.Validation(
                "Email.FormatInvalid",
                "Email format is invalid."
            );
    }
}
