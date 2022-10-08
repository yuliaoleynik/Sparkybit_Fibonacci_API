using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sparkybit_Fibonacci.Models
{
    [BsonIgnoreExtraElements]
    public class Log
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
