using System;

namespace Core.Models
{
    public class Event
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPublic { get; set; }
        public string Thumbnail { get; set; }
    }
}