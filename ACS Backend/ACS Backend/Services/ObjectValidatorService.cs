using System.Reflection;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using DB_Module.Attributes;

namespace ACS_Backend.Services;

public class ObjectValidatorService : IObjectValidatorService
{
    private ValidatorService _validator = new();

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
                var value = propertyInfo.GetValue(obj);

                if (attribute is RequiredAttribute)
                {
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()) ||
                        propertyInfo.PropertyType == typeof(Guid) && (Guid)value == Guid.Empty ||
                        propertyInfo.PropertyType == typeof(DateTime) && (DateTime)value == DateTime.MinValue ||
                        propertyInfo.PropertyType == typeof(int) && (int)value == 0 ||
                        propertyInfo.PropertyType == typeof(decimal) && (decimal)value == 0 ||
                        propertyInfo.PropertyType == typeof(double) && (double)value == 0 ||
                        propertyInfo.PropertyType == typeof(float) && (float)value == 0 ||
                        propertyInfo.PropertyType == typeof(long) && (long)value == 0 ||
                        propertyInfo.PropertyType == typeof(short) && (short)value == 0 ||
                        propertyInfo.PropertyType == typeof(byte) && (byte)value == 0 ||
                        propertyInfo.PropertyType == typeof(bool) && (bool)value == false ||
                        propertyInfo.PropertyType == typeof(char) && (char)value == '\0')
                    {
                        fails.Add($"{propertyInfo.Name} is required");
                    }
                }
                else if (attribute is EmailAddressAttribute)
                {
                    if (string.IsNullOrWhiteSpace(value.ToString()) == false &&
                        !_validator.ValidateEmail(value.ToString()))
                    {
                        fails.Add($"{propertyInfo.Name} is not a valid email address");
                    }
                }
                else if (attribute is PhoneNumberAttribute)
                {
                    if (string.IsNullOrWhiteSpace(value.ToString()) == false &&
                        !_validator.ValidatePhone(value.ToString()))
                    {
                        fails.Add($"{propertyInfo.Name} is not a valid phone number");
                    }
                }
                else if (attribute is BirthDateAttribute)
                {
                    if (value != null)
                    {
                        var birthDate = (DateTime)value;
                        if (!_validator.ValidateBirthDate(birthDate))
                        {
                            fails.Add($"{propertyInfo.Name} is not a valid birth date");
                        }
                    }
                    else
                    {
                        fails.Add($"Can't process {propertyInfo.Name}");
                    }
                }
            }
        }

        return fails.Count == 0
            ? new GenericResponseModel<List<string>> { QueryIsSuccess = true, Message = "All fields are valid" }
            : new GenericResponseModel<List<string>> { QueryIsSuccess = false, Data = fails };
    }
}