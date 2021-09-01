using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Models
{
    public class Venues
    {
        public static string GenerateUrl(double latitude, double longitude)
        {
            return string.Format(Constants.VENUE_BASE_URL, latitude, longitude, 
                Constants.VENUE_CLIENT_ID, Constants.VENUE_CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}
