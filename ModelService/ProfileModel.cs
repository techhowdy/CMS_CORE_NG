using System;
using System.Collections.Generic;

namespace ModelService
{
    public class ProfileModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string Displayname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string UserRole { get; set; }
        public string Lastname { get; set; }
        public bool IsTwoFactorOn { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsTermsAccepted { get; set; }
        public string ProfilePic { get; set; }
        public bool IsAccountLocked { get; set; }
        public bool IsEmployee { get; set; }
        public string UserId { get; set; }
        public ICollection<AddressModel> UseAddress { get; set; }
        public ICollection<ActivityModel> Activities { get; set; }
    }
}
