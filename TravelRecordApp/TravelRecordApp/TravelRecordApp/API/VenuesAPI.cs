using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Models;

namespace TravelRecordApp.API
{
    public class VenuesAPI
    {
        public static async Task<List<Venues>> GetVenues (double latitude, double longitude)
        {
            List<Venues> venues = new List<Venues>();
            using (HttpClient client = new HttpClient())
            {
                var url = Venues.GenerateUrl(latitude, longitude);
                var res = await client.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();
            }
            return venues;
        }
            


    }
}
