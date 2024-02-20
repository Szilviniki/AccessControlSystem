namespace ACS_Backend.Exceptions;

public class UniqueConstraintFailedException<T>:Exception
{
    public T? FailedOn { get; set; }
}