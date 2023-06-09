namespace UCABPagaloTodoMS.Application.CustomExceptions;

public class CustomException : Exception
{
    public CustomException()
    {
    }

    public CustomException(string message)
        : base(message)
    {
    }

    public CustomException(string message, Exception inner)
        : base(message, inner)
    {
    }
    public override string ToString()
    {
        return $"{typeof(CustomException).FullName}: {Message}\n{StackTrace}";
    }
}