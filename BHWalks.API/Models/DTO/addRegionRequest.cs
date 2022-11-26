using System.ComponentModel.DataAnnotations;

namespace BHWalks.API.Models.DTO
{
    public class AddRegionRequest
    {        
        public string RegionCode { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Population { get; set; }
    }
}
