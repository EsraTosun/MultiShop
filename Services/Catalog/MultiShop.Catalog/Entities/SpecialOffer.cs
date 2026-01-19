using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entites
{
    public class SpecialOffer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SpecialOfferId { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("SubTitle")]
        public string SubTitle { get; set; } = string.Empty;

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
