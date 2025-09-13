using OwnerService.Domain.Exceptions;
using System.Net.Mail;

namespace OwnerService.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidEmailFormat(value))
            {
                throw new OwnerDomainException("Invalid email format.");
            }
            Value = value;
        }

        private static bool IsValidEmailFormat(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new(value);
    }
}