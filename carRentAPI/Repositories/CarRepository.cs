using System;
using MongoDB.Driver;
using System.Runtime.ConstrainedExecution;
using carRentAPI.Models;

namespace carRentAPI.Repositories
{
    public class CarRepository
	{
        private readonly IMongoCollection<Vehicle> _carsCollection;

        public CarRepository(IMongoDatabase database)
        {
            _carsCollection = database.GetCollection<Vehicle>("cars");
        }

        public IEnumerable<Vehicle> GetCars() => _carsCollection.Find(car => true).ToList();

        public Vehicle GetCar(string id) => _carsCollection.Find(car => car.Id == id).FirstOrDefault();

        public void AddCar(Vehicle car) => _carsCollection.InsertOne(car);

        public void UpdateCar(string id, Vehicle updatedCar) =>
            _carsCollection.ReplaceOne(car => car.Id == id, updatedCar);

        public void DeleteCar(string id) =>
            _carsCollection.DeleteOne(car => car.Id == id);
    }
}

