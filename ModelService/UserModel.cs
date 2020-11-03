using System;
namespace ModelService
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfilePic { get; set; }
        public bool IsProfileComplete { get; set; }
        public bool IsActive { get; set; }
    }
}
