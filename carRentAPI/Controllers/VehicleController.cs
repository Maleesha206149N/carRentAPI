using System;
using System.Runtime.ConstrainedExecution;
using carRentAPI.Models;
using carRentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace carRentAPI.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class VehicleController : ControllerBase
	{
        private readonly CarRepository _carRepository;

        public VehicleController(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public IEnumerable<Vehicle> GetCars() => _carRepository.GetCars();

        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetCar(string id) => _carRepository.GetCar(id);

        [HttpPost]
        public ActionResult<Vehicle> AddCar([FromBody] Vehicle car)
        {
            car.Id = null;
            _carRepository.AddCar(car);
            return car;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCar(string id, [FromBody] Vehicle updatedCar)
        {
            _carRepository.UpdateCar(id, updatedCar);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(string id)
        {
            _carRepository.DeleteCar(id);
            return NoContent();
        }
    }
}

