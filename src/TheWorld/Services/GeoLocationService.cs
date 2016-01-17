using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheWorld.Services
{
    public class BingGeoLocationService
    {

        private readonly ILogger<BingGeoLocationService> _logger;
        //AsVSHH0xc-ZCgjCB4XbT56H7tJ0LvuUn_Bkmm0dPBxr1jtxk4vZ-LyRReqHA3tch        



        public BingGeoLocationService(ILogger<BingGeoLocationService> logger)
        {
            _logger = logger;
            //"AsVSHH0xc-ZCgjCB4XbT56H7tJ0LvuUn_Bkmm0dPBxr1jtxk4vZ-LyRReqHA3tch";

        }

        public async Task<GeoLocationServiceResult> Lookup(string location)
        {
            //var result = new GeoLocationServiceResult()
            //{
            //    Success = false,
            //    Message = "Undermined failure while looking up coordinates"
            //};
            var bingMapKey = Startup.Configuration["AppSettings:BingMapKey"];
            var encodedName = WebUtility.UrlEncode(location);
            string url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={bingMapKey}";
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);
            var jsonConverter = new JsonSerializer();
            var response = jsonConverter.Deserialize<Response>(new JsonTextReader(new StringReader(json)));
            GeoLocationServiceResult result;
            if (response.StatusCode == 200)
            {
                result = new GeoLocationServiceResult()
                {
                    Success = true,
                    Message = "Ok..!",
                    Latitude = response.ResourceSets.First().Resources.First().Point.Coordinates[0],
                    Longitude = response.ResourceSets.First().Resources.First().Point.Coordinates[1]
                };
                return result;
            }
            result = new GeoLocationServiceResult()
            {
                Success = false,
                Message = "Undermined failure while looking up coordinates"
            };
            return result;
        }
    }
}
