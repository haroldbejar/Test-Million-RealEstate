using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PropertyService.Domain.Enums;
using PropertyService.Domain.ValueObjects;

namespace PropertyService.Domain.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("codeowner")]
        public int CodeOwner { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("location")]
        public Location Location { get; set; } 

        [BsonElement("price")]
        public Price Price { get; set; } 

        [BsonElement("details")]
        public Details Details { get; set; } 

        [BsonElement("imageurl")]
        public string ImageUrl { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public PropertyStatus Status { get; set; } 

        [BsonElement("propertytype")]
        [BsonRepresentation(BsonType.String)]
        public PropertyType PropertyType { get; set; } 


    }
}