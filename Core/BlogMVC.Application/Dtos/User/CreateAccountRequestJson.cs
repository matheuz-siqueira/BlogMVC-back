namespace BlogMVC.Application.Dtos.User;

public class CreateAccountRequestJson
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
}
