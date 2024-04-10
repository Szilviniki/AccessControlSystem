namespace ACS_Backend.Exceptions;

public class BadFormatException : Exception
{
 public string Message { get; set; } = "Bad format";
}