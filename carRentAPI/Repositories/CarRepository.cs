using System;
using MongoDB.Driver;
using System.Runtime.ConstrainedExecution;
using carRentAPI.Models;

namespace carRentAPI.Repositories
{
    //car repository
    public class CarRepository
	{
        private readonly IMongoCollection<Vehicle> _carsCollection;

        public CarRepository(IMongoDatabase database)
        {
            _carsCollection = database.GetCollection<Vehicle>("cars");
        }

        // car details get repository
        public IEnumerable<Vehicle> GetCars() => _carsCollection.Find(car => true).ToList();

        // car deatails get by ID
        public Vehicle GetCar(string id) => _carsCollection.Find(car => car.Id == id).FirstOrDefault();

        //save car detail
        public void AddCar(Vehicle car) => _carsCollection.InsertOne(car);

        //update car detail
        public void UpdateCar(string id, Vehicle updatedCar) =>
            _carsCollection.ReplaceOne(car => car.Id == id, updatedCar);

        // delete car detail
        public void DeleteCar(string id) =>
            _carsCollection.DeleteOne(car => car.Id == id);
    }
}

