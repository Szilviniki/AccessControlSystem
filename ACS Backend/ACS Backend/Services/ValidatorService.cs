using ACS_Backend.Interfaces;
using System.Text.RegularExpressions;

namespace ACS_Backend.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool ValidateEmail(string email)
        {
            Regex regex = new Regex("[0-9A-Za-z.]{1,50}@[a-z0-9.]{2,40}[.][a-z]{2,5}");
            return regex.IsMatch(email);
        }

        public bool ValidatePhone(string phone)
        {
            Regex rPhone = new Regex("[+]36[1-9]0[0-9]{7}");
            return rPhone.IsMatch(phone);

        }

        public bool ValidateBirthDate(DateTime birthDate)
        {
            if (birthDate.Date> DateTime.Now.Date)
            {
                return false;
            }
            return true;
        }
    }
}