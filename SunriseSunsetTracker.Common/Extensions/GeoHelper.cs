namespace SunriseSunsetTracker.Common.Extensions;

public static class GeoHelper
{
    public static bool IsValidGeoCoordinate(double latitude, double longitude) =>
        latitude is >= -90 and <= 90 && longitude is >= -180 and <= 180;

}