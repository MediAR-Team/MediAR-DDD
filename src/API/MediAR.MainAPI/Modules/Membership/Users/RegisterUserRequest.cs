using System.ComponentModel.DataAnnotations;

namespace MediAR.MainAPI.Modules.Membership.Users
{
  public class RegisterUserRequest
  {
    [Required]
    public string UserName { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
  }
}
