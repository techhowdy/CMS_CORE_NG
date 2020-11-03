using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService
{
    public class CountryModel
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string TwoDigitCode { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public string Flag { get; set; }
        public string States { get; set; }
        [NotMapped]
        public string[] StatesList { get; set; }
    }
}
