using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Infrastructure.Enums;
using Infrastructure.Helper;

namespace Domain.Dto.Api
{
    public class PostCodeLocationDto
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PostCodeLocationDto()
        {
            District = string.Empty;
            Code = string.Empty;
        }

        /// <summary>
        /// Constructor with district value
        /// </summary>
        /// <param name="district">District value</param>
        public PostCodeLocationDto(string district)
            : this()
        {
            District = district;
        }
        public string Code { get; set; }
        public double DistanceMiles { get; set; }
        public double DistanceKm { get; set; }
        public string District { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        /// <summary>
        /// Set the distance between current object and second point geo coordinate
        /// </summary>
        /// <param name="geoCoordinate">Geo coordinate</param>
        public void SetDistance(GeoCoordinate geoCoordinate)
        {
            var currentGeoCoordinate = new GeoCoordinate(Latitude, Longitude);
            DistanceKm = DistanceHelper.DistanceTo(currentGeoCoordinate, geoCoordinate, DistanceUnit.Kilometer);
            DistanceMiles = DistanceHelper.DistanceTo(currentGeoCoordinate, geoCoordinate, DistanceUnit.Miles);
        }
    }
}