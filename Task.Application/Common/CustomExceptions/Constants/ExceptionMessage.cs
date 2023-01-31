namespace Task.Application.Common.CustomExceptions.Constants;

public static class ExceptionMessage
{
    // Task
    public const string DuplicateTaskTitle = "Task with this title already exists.";
    public const string TaskDateOverlapping = "Task start and end dates overlap with existing task";
    
    // User
    public const string DuplicateEmail = "Email already exists.";
    public const string InternalServerError = "Internal Server Error.";
    public const string UserDoesNotExist = "User does not exist.";
    public const string WrongEmailOrPassword = "Wrong email or password.";
    
    
    // Common
    public const string ResourceDoesNotExist = "Resource does not exist.";
}