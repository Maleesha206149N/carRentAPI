using System;
using Microsoft.AspNetCore.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace carRentAPI.Models
{
    public class UsersClass
    {

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Role")]
        public string? Role { get; set; }

        [BsonElement("NIC")]
        public string? NIC { get; set; }

    }
}

