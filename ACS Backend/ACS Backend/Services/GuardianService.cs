using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Utilities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace ACS_Backend.Services
{
    public class GuardianService : IGuardianService
    {
        private SQL _sql;
        private IObjectValidatorService _objectValidatorService;
        private IUniquenessChecker _uniquenessChecker;

        public GuardianService(SQL sql, IObjectValidatorService objectValidatorService,
            IUniquenessChecker uniquenessChecker)
        {
            _sql = sql;
            _objectValidatorService = objectValidatorService;
            _uniquenessChecker = uniquenessChecker;
        }

        public async Task AddGuardian(Guardian guardian)
        {
            var valResult = _objectValidatorService.Validate(guardian);

            if (valResult.QueryIsSuccess == false) throw new ArgumentException(string.Join(',', valResult.Data));

            var uResult = _uniquenessChecker.IsUniqueGuardian(guardian);

            if (uResult.QueryIsSuccess == false) throw new ArgumentException(string.Join(',', uResult.Data));

            _sql.Guardians.Add(guardian);
            await _sql.SaveChangesAsync();
        }

        public Task DeleteGuardian(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id can't be null");
            if (_sql.Guardians.Any(x => x.Id == id))
            {
                _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Id == id));
                return _sql.SaveChangesAsync();
            }
            else throw new ItemNotFoundException();
        }

        public Array GetAllGuardians()
        {
            return _sql.Guardians.ToArray();
        }

        public Guardian GetGuardian(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
            if (!_sql.Guardians.Any(x => x.Id == id)) throw new ItemNotFoundException();
            return _sql.Guardians.Single(x => x.Id == id);
        }

        public async Task UpdateGuardian(Guardian guardian, Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("No Id provided");
            if (!_sql.Guardians.Any(x => x.Id == id)) throw new ItemNotFoundException();

            if (string.IsNullOrWhiteSpace(guardian.Phone) && string.IsNullOrWhiteSpace(guardian.Email) &&
                string.IsNullOrWhiteSpace(guardian.Name))
                throw new ArgumentException("No data provided");

            var old = _sql.Guardians.FirstOrDefault(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(guardian.Email))
            {
                old.Email = guardian.Email;
            }

            if (!string.IsNullOrWhiteSpace(guardian.Phone))
            {
                old.Phone = guardian.Phone;
            }

            if (!string.IsNullOrWhiteSpace(guardian.Name))
            {
                old.Name = guardian.Name;
            }

            var vRes = _objectValidatorService.Validate(old);
            if (vRes.QueryIsSuccess == false)
                throw new ArgumentException(string.Join(',', vRes.Data));

            if (old != null) _sql.Guardians.Update(old);
            await _sql.SaveChangesAsync();
        }
    }
}