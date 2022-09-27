using GeoCoordinatePortable;
using Infrastructure.Enums;
using Infrastructure.Helper;

namespace UnitTest.Infrastructure;

public class DistanceHelperTest
{
    [Fact]
    public void DistanceHelper_CalculateDistanceBetweenTwoDistancesKM()
    {
        var pointOne = new GeoCoordinate(51.560414, -0.116805);
        var pointTwo = new GeoCoordinate(51.4700223, -0.4542955);

        var result = DistanceHelper.DistanceTo(pointOne, pointTwo, DistanceUnit.Kilometer);

        Assert.Equal(25.45, result);
    }

    [Fact]
    public void DistanceHelper_CalculateDistanceBetweenTwoDistancesMiles()
    {
        var pointOne = new GeoCoordinate(51.560414, -0.116805);
        var pointTwo = new GeoCoordinate(51.4700223, -0.4542955);

        var result = DistanceHelper.DistanceTo(pointOne, pointTwo, DistanceUnit.Miles);

        Assert.Equal(15.81, result);
    }
    
    [Fact]
    public void DistanceHelper_CalculateDistanceBetweenTwoDistancesMeters()
    {
        //25446.475406607013 in meters
        var pointOne = new GeoCoordinate(51.560414, -0.116805);
        var pointTwo = new GeoCoordinate(51.4700223, -0.4542955);

        var result = DistanceHelper.DistanceTo(pointOne, pointTwo, DistanceUnit.Meters);

        Assert.Equal(25446.48, result);
    }
}