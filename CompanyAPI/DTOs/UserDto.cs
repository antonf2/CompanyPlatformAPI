using CompanyAPI.Data.Entities;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }

    public UserDto() { }

    public UserDto(User user)
    {
        if (user != null)
        {
            Id = user.UserId;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            IsActive = user.IsActive;
        }
    }
}
