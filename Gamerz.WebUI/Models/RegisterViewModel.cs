using System.ComponentModel.DataAnnotations;

namespace Gamerz.WebUI.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.")]
        public string PasswordConfirm { get; set; }

    }
}
