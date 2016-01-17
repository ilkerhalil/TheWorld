namespace TheWorld.Services
{
    public class GeoLocationServiceResult
    {
        public bool Success { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Message { get; set; }

    }
    
}