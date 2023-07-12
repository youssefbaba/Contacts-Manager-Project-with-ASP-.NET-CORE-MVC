using ContactsManager.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Person Name can't be blank.")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email Address can't be blank.")]
        [EmailAddress(ErrorMessage = "Email Address should be a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already registered")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone Number can't be blank.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number should contain numbers only.")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password can't be blank.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password can't be blank.")]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password should be the same.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }

        // By default all users are registered as regular users (Role = User) 
        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;

    }
}
