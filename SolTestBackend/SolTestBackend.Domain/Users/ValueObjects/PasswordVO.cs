using System.Text.RegularExpressions;

using SolTestBackend.Core.Interfaces;
using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Users.Errors;

namespace SolTestBackend.Domain.Users.ValueObjects
{
    public record PasswordVO
    {
        private static readonly string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
        public string Hash { get; }
        
        private PasswordVO(string hash)
        {
            Hash = hash;
        }

        public static PasswordVO Create(string plainText, IPasswordHasher hasher)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                throw new DomainException(PasswordError.IsEmpty);
            }

            if (IsInvalidFormat(plainText))
            {
                throw new DomainException(PasswordError.FormatInvalid);
            }

            return new PasswordVO(hasher.Hash(plainText));
        }

        public static PasswordVO FromDB(string hash)
        {
            return new PasswordVO(hash);
        }

        private static bool IsInvalidFormat(string plainText)
        {
            return !Regex.IsMatch(plainText, PasswordPattern);
        }
    }
}
