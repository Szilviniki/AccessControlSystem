using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;

namespace ACS_Backend.Services
{
    public class RestrictionService : IRestrictionService
    {
        private SQL _sql;
        public RestrictionService(SQL sql)
        {
            _sql = sql;
        }

        public async Task CreateRestriction(Restriction restriction)
        {
            if (_sql.Restrictions.Any(restriction1 => restriction1.Name == restriction.Name)) throw new ItemAlreadyExistsException();

            _sql.Restrictions.Add(restriction);
            await _sql.SaveChangesAsync();
        }

        public async Task DeleteRestriction(int id)
        {
            if (_sql.Restrictions.Any(x => x.Id == id))
            {
                _sql.Remove(_sql.Restrictions.Single(x => x.Id == id));
                await _sql.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public Restriction GetRestrictionById(int id)
        {
            if (!_sql.Restrictions.Any(x => x.Id == id)) throw new ItemNotFoundException();
            return _sql.Restrictions.Single(x => x.Id == id);
        }

        public Array GetRestrictions()
        {
            return _sql.Restrictions.ToArray();
        }

    }
}
