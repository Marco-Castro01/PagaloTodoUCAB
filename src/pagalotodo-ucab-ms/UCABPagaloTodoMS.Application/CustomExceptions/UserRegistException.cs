namespace UCABPagaloTodoMS.Application.CustomExceptions;

public class UserRegistException : Exception
{
    public UserRegistException()
    {
    }

    public UserRegistException(string message)
        : base(message)
    {
    }

    public UserRegistException(string message, Exception inner)
        : base(message, inner)
    {
    }
}