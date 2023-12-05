using System;
using carRentAPI.Models;
using MongoDB.Driver;

namespace carRentAPI.Repositories
{
    //booking repository
	public class BookingRepository
	{
        private readonly IMongoCollection<Booking> _bookingsCollection;

        public BookingRepository(IMongoDatabase database)
        {
            _bookingsCollection = database.GetCollection<Booking>("bookings");
        }
        
        //get booking detail repository
        public IEnumerable<Booking> GetBookings() => _bookingsCollection.Find(booking => true).ToList();

        //get booking detail by ID
        public Booking GetBooking(string id) => _bookingsCollection.Find(booking => booking.Id == id).FirstOrDefault();

        //Add booking detail
        public void AddBooking(Booking booking) => _bookingsCollection.InsertOne(booking);

        // update booking information
        public void UpdateBooking(string id, Booking updatedBooking) =>
            _bookingsCollection.ReplaceOne(booking => booking.Id == id, updatedBooking);
        // delete booking detail
        public void DeleteBooking(string id) =>
            _bookingsCollection.DeleteOne(booking => booking.Id == id);
    }
}

