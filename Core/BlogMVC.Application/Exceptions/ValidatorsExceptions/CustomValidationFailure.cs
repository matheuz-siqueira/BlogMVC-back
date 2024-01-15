namespace BlogMVC.Application.Exceptions.ValidatorsExceptions;

public class CustomValidationFailure  
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
    public CustomValidationFailure(string propertyName, string errorMessage)
    {
        PropertyName = propertyName; 
        ErrorMessage = errorMessage; 
    }
}
