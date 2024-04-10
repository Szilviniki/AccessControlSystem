namespace ACS_Backend.Exceptions;

public class UnprocessableEntityException : Exception
{
    public string? Message { get; set; }
}