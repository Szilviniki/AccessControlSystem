using ACS_Backend.Interfaces;
using System.Text.RegularExpressions;

namespace ACS_Backend.Services
{
    public class MatchingService : IMatchingService
    {
        public bool MatchEmail(string email)
        {
            Regex regex = new Regex("[0-9A-Za-z.]{1,50}@[a-z0-9.]{2,50}[.][a-z]{2,5}");
            return regex.IsMatch(email);
        }

        public bool MatchPhone(string phone)
        {
            Regex rPhone = new Regex("[+]36[1-9]0[0-9]{7}");
            return rPhone.IsMatch(phone);
        }
    }
}