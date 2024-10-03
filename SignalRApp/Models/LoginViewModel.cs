using System.ComponentModel.DataAnnotations;

namespace SignalRApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter username")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [StringLength(50)]
        public string Password { get; set; }
    }

}
