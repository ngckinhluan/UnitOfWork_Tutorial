using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessObjects.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!; // MongoDB identifier

        [BsonElement("OrderId")]
        public required int OrderId { get; set; }

        [BsonElement("OrderName")]
        public string? OrderName { get; set; }

        [BsonElement("Quantity")]
        public int? Quantity { get; set; }
    }
}