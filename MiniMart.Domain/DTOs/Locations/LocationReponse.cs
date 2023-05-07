namespace MiniMart.Domain.DTOs.Locations
{
    public class LocationReponse
    {
        public LocationReponse()
        {
        }
        

    }

    public class CityResponse
    {
        public CityResponse()
        {
            Districs = new List<DistricResponse>();
        }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<DistricResponse> Districs { get; set; } 
    }

    public class DistricResponse
    {
        public DistricResponse()
        {
            Wards = new List<WardResponse>();
        }
        public int DistricId { get; set; }
        public string DistricName { get; set;}
        public List<WardResponse> Wards { get; set; }
    }

    public class WardResponse
    {
        public int WardId { get; set;}
        public string WardName { get; set;}
        public int Quantity { get; set;}
    }
}
