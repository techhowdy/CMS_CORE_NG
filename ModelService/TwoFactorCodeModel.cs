using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService
{
    public class TwoFactorCodeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TwoFactorCode { get; set; }

        [Required]
        public bool RememberDevice { get; set; }

        [Required]
        public string SelectedProvider { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string EncryptionKey2Fa { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public int Attempts { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public bool CodeExpired { get; set; }

        [Required]
        public bool CodeIsUsed { get; set; }

        [Required]
        public string UserAgent { get; set; }

        [Required]
        public string EncryptionKeyForDeviceId { get; set; }

        [Required]
        public DateTime DeviceIdExpiry { get; set; }

        [Required]
        public bool IsDeviceIdExpired { get; set; } 

        [NotMapped]
        public bool AuthCodeRequired { get; set; }
    }
}
