using System.ComponentModel.DataAnnotations;

namespace MediAR.MainAPI.Modules.Membership.Users
{
  public class CreateNewUserRequest
  {
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
  }
}
