using System;
using System.ComponentModel.DataAnnotations;

namespace ModelService
{
    public class ActivityModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public string OperatingSystem { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
    }
}
