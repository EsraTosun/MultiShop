using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entites
{
    public class FeatureSlider
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FeatureSliderId { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonElement("Status")]
        public bool Status { get; set; } = true;
    }
}
