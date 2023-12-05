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

        //vehicle detail get endpoint
        [HttpGet]
        public IEnumerable<Vehicle> GetCars() => _carRepository.GetCars();

        //get vehicle detail by ID
        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetCar(string id) => _carRepository.GetCar(id);

        //submit vehicle detail
        [HttpPost]
        public ActionResult<Vehicle> AddCar([FromBody] Vehicle car)
        {
            car.Id = null;
            _carRepository.AddCar(car);
            return car;
        }

        //update vehicle detail
        [HttpPut("{id}")]
        public IActionResult UpdateCar(string id, [FromBody] Vehicle updatedCar)
        {
            _carRepository.UpdateCar(id, updatedCar);
            return NoContent();
        }

        //delete vehicle detail
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(string id)
        {
            _carRepository.DeleteCar(id);
            return NoContent();
        }
    }
}

