using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Utilities;

namespace ACS_Backend.Services
{
    public class GuardianService : IGuardianService
    {
        private SQL _sql;
        public GuardianService(SQL sql) { _sql = sql; }

        public async Task AddGuardian(Guardian guardian)
        {
            if (!_sql.Parents.Any(x => x.Phone == guardian.Phone)) throw new ItemAlreadyExistsException();
            _sql.Parents.Add(guardian);
            await _sql.SaveChangesAsync();
        }

        public Task DeleteGuardian(Guid id)
        {
            if (_sql.Parents.Any(x => x.Id == id))
            {
                _sql.Parents.Remove(_sql.Parents.Single(x => x.Id == id));
                return _sql.SaveChangesAsync();
            }else throw new ItemNotFoundException();
        }

        public Array GetAllGuardians()
        {
            return _sql.Parents.ToArray();
        }

        public Guardian GetGuardian(Guid id)
        {
           if(!_sql.Parents.Any(x=> x.Id == id)) throw new ItemNotFoundException();
                return _sql.Parents.Single(x => x.Id == id);

        }

        public async Task UpdateGuardian(Guardian guardian)
        {
            if(!_sql.Parents.Any(x=> x.Id == guardian.Id)) throw new ItemNotFoundException();
            if(_sql.Parents.Any(x=> x.Phone == guardian.Phone&&x.Name==guardian.Name)) throw new ItemAlreadyExistsException();
            _sql.Parents.Update(guardian);
            await _sql.SaveChangesAsync();
        }
    }
}
