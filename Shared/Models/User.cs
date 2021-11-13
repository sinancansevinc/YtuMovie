using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(80,ErrorMessage ="Your password must be beetween {2} and {1} characters.",MinimumLength =6)]
        [Display(Name ="Password")]
        public string Password { get; set; }

    }
}
