using System.ComponentModel.DataAnnotations;

namespace MediAR.MainAPI.Modules.Membership.Authentication
{
  public class AuthenticateRequest
  {
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
  }
}
