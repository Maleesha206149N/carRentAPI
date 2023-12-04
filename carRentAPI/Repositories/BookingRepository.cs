using System;
using carRentAPI.Models;
using MongoDB.Driver;

namespace carRentAPI.Repositories
{
	public class BookingRepository
	{
        private readonly IMongoCollection<Booking> _bookingsCollection;

        public BookingRepository(IMongoDatabase database)
        {
            _bookingsCollection = database.GetCollection<Booking>("bookings");
        }

        public IEnumerable<Booking> GetBookings() => _bookingsCollection.Find(booking => true).ToList();

        public Booking GetBooking(string id) => _bookingsCollection.Find(booking => booking.Id == id).FirstOrDefault();

        public void AddBooking(Booking booking) => _bookingsCollection.InsertOne(booking);

        public void UpdateBooking(string id, Booking updatedBooking) =>
            _bookingsCollection.ReplaceOne(booking => booking.Id == id, updatedBooking);

        public void DeleteBooking(string id) =>
            _bookingsCollection.DeleteOne(booking => booking.Id == id);
    }
}

