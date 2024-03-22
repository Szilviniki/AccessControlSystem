namespace ACS_Backend.Exceptions;

public class BadFormatException : Exception
{
    public KeyValuePair<string, string>? failedField { get; set; }
}