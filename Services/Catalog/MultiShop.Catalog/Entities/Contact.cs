using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entites
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactId { get; set; }

        [BsonElement("NameSurname")]
        public string NameSurname { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Subject")]
        public string Subject { get; set; } = string.Empty;

        [BsonElement("Message")]
        public string Message { get; set; } = string.Empty;

        [BsonElement("IsRead")]
        public bool IsRead { get; set; } = false;

        [BsonElement("SendDate")]
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
    }
}
