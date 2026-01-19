using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entites
{
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductDetailId { get; set; }

        [BsonElement("ProductDescription")]
        public string ProductDescription { get; set; } = string.Empty;

        [BsonElement("ProductInfo")]
        public string ProductInfo { get; set; } = string.Empty;

        public string ProductId { get; set; } = string.Empty;

        [BsonIgnore]
        public Product Product { get; set; }
    }
}
