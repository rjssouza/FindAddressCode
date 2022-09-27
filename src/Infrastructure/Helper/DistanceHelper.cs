using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Infrastructure.Enums;

namespace Infrastructure.Helper
{
    public static class DistanceHelper
    {
        private const double KILOMETER_CONSTANT = 1000;
        private const double MILES_CONSTANT = 1609.344;

        /// <summary>
        /// Calculate distance from one point to another
        /// </summary>
        /// <param name="start">Start point</param>
        /// <param name="destination">Destination point</param>
        /// <param name="unit">Distance unit, can be KM, Meter or Miles</param>
        /// <returns>Distance between two points</returns>
        public static double DistanceTo(GeoCoordinate start, GeoCoordinate destination, DistanceUnit unit = DistanceUnit.Kilometer)
        {
            var distanceInMeters = start.GetDistanceTo(destination);
            if (distanceInMeters <= 0)
                return distanceInMeters;

            switch (unit)
            {
                case DistanceUnit.Kilometer:
                    return Math.Round(distanceInMeters / KILOMETER_CONSTANT, 2);
                case DistanceUnit.Miles:
                    return Math.Round(distanceInMeters / MILES_CONSTANT, 2);
                default:
                    return Math.Round(distanceInMeters, 2);
            }
        }
    }
}