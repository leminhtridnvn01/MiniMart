namespace MiniMart.Domain.DTOs.Stores
{
    public class GetLocationManageStoreResponse
    {
        public GetLocationManageStoreResponse()
        {

        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public IEnumerable<GetStoreLocationResponse> Stores { get; set; }
    }
}
