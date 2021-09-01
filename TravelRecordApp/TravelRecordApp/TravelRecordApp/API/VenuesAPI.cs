using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Models;
using TravelRecordApp.Helpers;
using Newtonsoft.Json;

namespace TravelRecordApp.API
{
    public class VenuesAPI
    {
        private static string GenerateUrl(double latitude, double longitude)
        {
            return string.Format(Constants.VENUE_BASE_URL, latitude, longitude,
                Constants.VENUE_CLIENT_ID, Constants.VENUE_CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }

        public static async Task<List<Venue>> GetVenues (double latitude, double longitude)
        {
            List<Venue> venues = new List<Venue>();
            using (HttpClient client = new HttpClient())
            {
                var url = GenerateUrl(latitude, longitude);
                var res = await client.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();
                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = venueRoot.response.venues;
            }
            return venues;
        }


    }
}
