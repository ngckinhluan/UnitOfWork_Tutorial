using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessObjects.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!; // MongoDB identifier

        [BsonElement("ProductId")]
        public required int ProductId { get; set; }

        [BsonElement("ProductName")]
        public string? ProductName { get; set; }

        [BsonElement("Price")]
        public decimal? Price { get; set; }

        [BsonElement("Stock")]
        public int? Stock { get; set; }
    }
}