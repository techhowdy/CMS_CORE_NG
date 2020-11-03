using System;
using System.ComponentModel.DataAnnotations;

namespace ModelService
{
    public class PermissionType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
