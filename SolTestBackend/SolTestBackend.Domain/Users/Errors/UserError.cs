using SolTestBackend.Core.Results;

namespace SolTestBackend.Domain.Users.Errors
{
    public static class UserError
    {
        public static Error NameIsRequired=>
            Error.Validation(
                "User.Name.Required",
                "User name is required"
            );
    }
}
