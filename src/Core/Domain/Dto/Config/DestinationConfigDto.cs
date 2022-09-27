using GeoCoordinatePortable;

namespace Domain.Dto.Config
{
    public class DestinationConfigDto
    {
        public DestinationConfigDto()
        {
            Name = string.Empty;
        }

        public DestinationConfigDto(string name)
        {
            Name = name;
        }
        
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitute { get; set; }
        public GeoCoordinate GeoCoordinate => new GeoCoordinate(Latitude, Longitute);
    }
}