using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface IObjectValidatorService
{
   public GenericResponseModel<List<string>> Validate<TObject>(TObject obj) where TObject : class;
}