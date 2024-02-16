namespace ACS_Backend.Model;

public class GenericResponseModel<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool QueryIsSuccess { get; set; } = true;
}