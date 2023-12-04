using System;
using carRentAPI.Models;
using carRentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace carRentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
	{
        private readonly BookingRepository _bookingRepository;

        public BookingController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IEnumerable<Booking> GetBookings() => _bookingRepository.GetBookings();

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(string id) => _bookingRepository.GetBooking(id);

        [HttpPost]
        public ActionResult<Booking> AddBooking([FromBody] Booking booking)
        {
            booking.Id = null;
            _bookingRepository.AddBooking(booking);
            return booking;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(string id, [FromBody] Booking updatedBooking)
        {
            _bookingRepository.UpdateBooking(id, updatedBooking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(string id)
        {
            _bookingRepository.DeleteBooking(id);
            return NoContent();
        }

    }
}

