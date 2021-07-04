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
    }
}
