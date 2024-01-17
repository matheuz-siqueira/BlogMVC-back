namespace BlogMVC.Application.Dtos.User;

public class UpdatePasswordRequestJson
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
