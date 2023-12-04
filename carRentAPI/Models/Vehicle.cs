using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace carRentAPI.Models
{
	public class Vehicle
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal DailyRate { get; set; }
        public string IsAvailable { get; set; }
        public string ContactNo { get; set; }
        public string Location { get; set; }
        public string ImageData { get; set; }
    }
}

