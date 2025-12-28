namespace BlazorApp.utilities
{
    public static class GeoUtils
    {
        private const double radius = 6378137;  // radius of earth


        public static Dictionary<int, int> FindDistances (List<Anomaly> Sensor1, List<Anomaly> Sensor2)
        {
            /*
            Input: Sensor1 and Sensor2 decimal degree data
            Output: list of ID's for points within 200m of each other
            */

            double distance;
            Dictionary<int, int> idPairs = new Dictionary<int, int>();

            foreach (Anomaly point1 in Sensor1)
            {
                // check if point is in valid range
                if (point1.latitude >= -90 && point1.latitude <= 90 && point1.longitude >= -180 && point1.longitude <= 180)
                {
                    foreach (Anomaly point2 in Sensor2)
                    {
                        // check if point is in valid range
                        if (point2.latitude >= -90 && point2.latitude <= 90 && point2.longitude >= -180 && point2.longitude <= 180)
                        {
                            distance = DistanceWGS84(
                            point1.latitude,
                            point1.longitude,
                            point2.latitude,
                            point2.longitude);

                            if (distance <= 200)    // tolerance of 100m for both points
                            {
                                idPairs[point1.id] = point2.id;
                            }
                        }
                    }
                }
            }
            return idPairs;
        }

        private static double DistanceWGS84(double lat1, double lon1, double lat2, double lon2)
        {
            /*
            Input: latitude and longitude of two points on earth
            Output: distance between the points in meters
            */

            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dLon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            return radius * a;  //distance in meters
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180;
        }

    }
}