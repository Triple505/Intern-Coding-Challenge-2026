namespace BlazorApp.utilities
{
    public class Anomaly
    {
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Anomaly(int id, double latitude, double longitude)
        {
            this.id = id;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}