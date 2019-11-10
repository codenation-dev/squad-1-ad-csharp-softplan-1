using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CentralDeErros.Domain.Models
{
    public class ErrorLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public bool Shelved { get; set; }
        public string Environment { get; set; }

    }
}
