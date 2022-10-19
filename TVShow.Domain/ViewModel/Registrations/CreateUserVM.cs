using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TVShow.Domain.ViewModel
{
    public class CreateUserVM
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(14, ErrorMessage = "Must be between 6 and 14 characters", MinimumLength = 6 )]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8}$", ErrorMessage = "Password must meet requirements")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Your Password needs to be equals to ConfirmPassword, please try again")]
        [StringLength(14, ErrorMessage = "Must be between 6 and 14 characters", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
