using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entites
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonElement("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        [BsonElement("ProductPrice")]
        public decimal ProductPrice { get; set; } = 0;

        [BsonElement("ProductImageUrl")]
        public string ProductImageUrl { get; set; } = string.Empty;

        [BsonElement("ProductDescription")]
        public string ProductDescription { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = string.Empty;

        [BsonIgnore]
        public Category Category { get; set; }
    }
}
