using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entites
{
    public class ProductImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductImageID { get; set; }

        [BsonElement("Image1")]
        public string Image1 { get; set; } = string.Empty;

        [BsonElement("Image2")]
        public string Image2 { get; set; } = string.Empty;

        [BsonElement("Image3")]
        public string Image3 { get; set; } = string.Empty;

        [BsonElement("Image4")]
        public string Image4 { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;

        [BsonIgnore]
        public Product Product { get; set; }
    }
}
