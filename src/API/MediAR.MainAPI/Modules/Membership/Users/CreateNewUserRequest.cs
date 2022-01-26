namespace MediAR.MainAPI.Modules.Membership.Users
{
  public class CreateNewUserRequest
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
  }
}
