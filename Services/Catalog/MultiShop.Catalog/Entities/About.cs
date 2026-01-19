using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations; 

namespace MultiShop.Catalog.Entites
{
    public class About
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AboutId { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty; // boş gelmesini önlemek için default

        [BsonElement("Address")]
        public string Address { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Phone")]
        public string Phone { get; set; } = string.Empty;
    }
}
