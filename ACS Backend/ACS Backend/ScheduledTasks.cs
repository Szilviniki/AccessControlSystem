using FluentScheduler;

namespace ACS_Backend
{
    public class ScheduledTasks
    {
        private SQL _sql;

        public ScheduledTasks(SQL sql)
        {
            _sql = sql;
        }
        public async void EveryoneOut()
        {
            var facultyInside = _sql.Personnel.Where(x => x.IsPresent == true);
            var studentsInside = _sql.Students.Where(x => x.IsPresent == true);

            foreach (var student in studentsInside)
            {
                student.IsPresent = false;
                _sql.Students.Update(student);
            }

            foreach (var faculty in facultyInside)
            {
                faculty.IsPresent = false;
                _sql.Personnel.Update(faculty);
            }

            await _sql.SaveChangesAsync();
            int remaining = _sql.Personnel.Count(x=>x.IsPresent == true);
            remaining += _sql.Students.Count(x => x.IsPresent == true);
            Console.WriteLine($"Remaining people: {remaining}");
        }
    }
}
