using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieManager.Services.DomainModels
{
    public class DbMovie : IIdentifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("director")]
        public string Director { get; set; }

        // TODO map as a list if needed
        [BsonElement("actors")]
        public string Actors { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }
    }
}
