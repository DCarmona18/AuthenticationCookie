using System.ComponentModel.DataAnnotations;

namespace AuthenticationCookie
{
    public record UserDTO([Required] string Username, [Required] string Password);
}
