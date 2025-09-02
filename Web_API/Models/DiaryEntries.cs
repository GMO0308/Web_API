using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Web_API.Models
{
    public class DiaryEntries
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
