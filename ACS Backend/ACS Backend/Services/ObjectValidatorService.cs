using System.Reflection;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using DB_Module.Attributes;

namespace ACS_Backend.Services;

public class ObjectValidatorService : IObjectValidatorService
{
    private ValidatorService _validator = new ValidatorService();

    public GenericResponseModel<List<string>> Validate<TObject>(TObject obj) where TObject : class
    {
        var results = new GenericResponseModel<List<string>>();
        var fails = new List<string>();
        PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            object[] attributes = propertyInfo.GetCustomAttributes(true);
            foreach (object attribute in attributes)
            {
                if (attribute is RequiredAttribute)
                {
                    var value = propertyInfo.GetValue(obj);
                    if (value == null ||
                        string.IsNullOrWhiteSpace(value.ToString()) ||
                        propertyInfo.PropertyType == typeof(Guid) && (Guid)value == Guid.Empty)
                    {
                        fails.Add($"{propertyInfo.Name} is required");
                    }
                }
                else if (attribute is EmailAddressAttribute)
                {
                    string email = propertyInfo.GetValue(obj).ToString();
                    if (!_validator.ValidateEmail(email))
                    {
                        fails.Add($"{propertyInfo.Name} is not a valid email address");
                    }
                }
                else if (attribute is PhoneNumberAttribute)
                {
                    string phone = propertyInfo.GetValue(obj).ToString();
                    if (!_validator.ValidatePhone(phone))
                    {
                        fails.Add($"{propertyInfo.Name} is not a valid phone number");
                    }
                }
                else if (attribute is BirthDateAttribute)
                {
                    DateTime birthDate = (DateTime)propertyInfo.GetValue(obj);
                    if (!_validator.ValidateBirthDate(birthDate))
                    {
                        fails.Add($"{propertyInfo.Name} is not a valid birth date");
                    }
                }
            }
        }

        
        if (fails.Count != 0)
        {
            results.QueryIsSuccess = false;
            results.Data = fails;
            return results;
        }else
        {
            results.Message = "Everything is ok";
            return results;
        }
    }
}