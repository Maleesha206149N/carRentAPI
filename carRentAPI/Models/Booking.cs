using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace carRentAPI.Models
{
    //Booking model
	public class Booking
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CarId { get; set; }
        public string Name { get; set; }
        public string mobileNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

