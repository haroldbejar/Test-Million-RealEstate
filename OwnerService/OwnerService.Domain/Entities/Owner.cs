using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OwnerService.Domain.ValueObjects;

namespace OwnerService.Domain.Entities
{
    public class Owner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("ownercode")]
        public OwnerCode OwnerCode { get; private set; }

        [BsonElement("fullname")]
        public string FullName { get; private set; }

        [BsonElement("address")]
        public Address Address { get; private set; }

        [BsonElement("phone")]
        public string Phone { get; private set; }

        [BsonElement("email")]
        public Email Email { get; private set; }

        private Owner() { }

        public static Owner Create(OwnerCode ownerCode, string fullName, Address address, string phone, Email email)
        {
            var owner = new Owner
            {
                OwnerCode = ownerCode,
                FullName = fullName,
                Address = address,
                Phone = phone,
                Email = email
            };
            return owner;
        }

        public void ChangeAddress(Address newAddress)
        {
            Address = newAddress;
        }

        public void ChangeEmail(Email newEmail)
        {
            Email = newEmail;
        }

        public void ChangePhone(string newPhone)
        {
            Phone = newPhone;
        }
    }
}
