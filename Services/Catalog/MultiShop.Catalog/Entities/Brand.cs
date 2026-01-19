using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entites
{
    public class Brand
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BrandId { get; set; }

        [BsonElement("BrandName")]
        public string BrandName { get; set; } = string.Empty; // boş gelmesini önlemek için default

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } 
    }
}
