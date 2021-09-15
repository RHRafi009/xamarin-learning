using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Models
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Experience { get; set; }
        public string VenueId { get; set; }
        public string VenueName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
