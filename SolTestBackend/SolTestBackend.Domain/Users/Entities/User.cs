using SolTestBackend.Core.Results;
using SolTestBackend.Core.Abstractions;

using SolTestBackend.Domain.Users.Errors;
using SolTestBackend.Domain.Users.ValueObjects;

namespace SolTestBackend.Domain.Users.Entities
{
    public class User : AggregateRoot
    {
        public string Name { get; private set; }
        public EmailVO Email { get; private set; }
        public PasswordVO Password { get; private set; }

        private User()
        {
        }

        private User(string name, EmailVO email, PasswordVO password):
            base(Guid.NewGuid()) {
            Name = name;
            Email = email;
            Password= password;
        }

        public static User Create (string name, EmailVO email, PasswordVO password)
        {
            ValidateName(name);
            return new User(name, email, password);
        }

        public void SetName(string name)
        {
            ValidateName(name);
            Name = name;
            MarkAsUpdated();
        }

        public void SetEmail(EmailVO email)
        {
            Email = email;
            MarkAsUpdated();
        }
        
        public void SetPassword(PasswordVO password)
        {
            Password = password;
            MarkAsUpdated();
        }

        private static void ValidateName(string name)
        {
            if (IsInvalidName(name))
            {
                throw new DomainException(UserError.NameIsRequired);
            }
        }

        private static bool IsInvalidName(string name)
        {
            return string.IsNullOrWhiteSpace(name);
        }

    }
}
