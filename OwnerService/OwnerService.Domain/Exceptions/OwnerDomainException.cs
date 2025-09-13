using System;
using OwnerService.Domain.ValueObjects;

namespace OwnerService.Domain.Exceptions
{
    public sealed class OwnerDomainException : Exception
    {
        public string Code { get; }

        public OwnerDomainException(string message) : base(message)
        {
            Code = "OWNER_DOMAIN_ERROR";
        }

        public OwnerDomainException(string code, string message) : base(message)
        {
            Code = code;
        }

        public OwnerDomainException(string message, Exception innerException) : base(message, innerException)
        {
            Code = "OWNER_DOMAIN_ERROR";
        }

        public OwnerDomainException(string code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        // Métodos estáticos para crear excepciones específicas
        public static OwnerDomainException OwnerNotFound(string ownerId) =>
            new("OWNER_NOT_FOUND", $"Owner with ID '{ownerId}' was not found");

        public static OwnerDomainException OwnerCodeNotFound(OwnerCode ownerCode) =>
            new("OWNER_CODE_NOT_FOUND", $"Owner with code '{ownerCode}' was not found");

        public static OwnerDomainException EmailAlreadyExists(Email email) =>
            new("EMAIL_ALREADY_EXISTS", $"Email '{email}' is already registered");

        public static OwnerDomainException OwnerCodeAlreadyExists(OwnerCode ownerCode) =>
            new("OWNER_CODE_ALREADY_EXISTS", $"Owner code '{ownerCode}' already exists");

        public static OwnerDomainException InvalidOwnerData(string details) =>
            new("INVALID_OWNER_DATA", $"Invalid owner data: {details}");
    }
}