using System.ComponentModel.DataAnnotations;

namespace ModelService
{
    public class AddUserModel
    {
        [Required(ErrorMessage = "Email address required")]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username required")]
        [Display(Name = "Username")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]{3,9}$", ErrorMessage = "Please enter a valid password")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First name required")]
        [Display(Name = "First name")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Birthday required")]
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePic { get; set; }

        [Required(ErrorMessage = "User Role required")]
        public string UserRole { get; set; }
        public AddressModel BillingAddress { get; set; }
        public AddressModel ShippingAddress { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "You need to accept the terms!")]
        [Display(Name = "Terms & Conditions")]
        public bool IsTermsAccepted { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsTwoFactorOn { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsAccountLocked { get; set; }
        public bool IsEmployee { get; set; }
    }
}
