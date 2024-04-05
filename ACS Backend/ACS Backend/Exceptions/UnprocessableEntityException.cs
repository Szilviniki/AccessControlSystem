namespace ACS_Backend.Exceptions;

public class UnprocessableEntityException : Exception
{
    public string? Details { get; set; }
}