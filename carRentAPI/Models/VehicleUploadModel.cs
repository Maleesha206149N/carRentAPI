using System;
namespace carRentAPI.Models
{
	public class VehicleUploadModel
	{
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal DailyRate { get; set; }
        public string IsAvailable { get; set; }
        public string ContactNo { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
    }
}

