using System.Text.RegularExpressions;

using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Users.Errors;

namespace SolTestBackend.Domain.Users.ValueObjects
{
    public record EmailVO
    {
        private static readonly string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public string Email { get; }

        private EmailVO(string email)
        {
            Email = email;
        }

        public static EmailVO Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(EmailError.IsEmpty);
            }

            if (IsInvalidFormat(email))
            {
                throw new DomainException(EmailError.FormatInvalid);
            }

            return new EmailVO(email);
        }

        private static bool IsInvalidFormat(string email)
        {
            return !Regex.IsMatch(email, EmailPattern);
        }
    }
}
