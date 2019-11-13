using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CentralDeErros.Domain.Models.Base
{
    public class ModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
