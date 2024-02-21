using ACS_Backend.Interfaces;
using FluentScheduler;

namespace ACS_Backend.Utilities
{
    public class SchedueldTasks
    {
        private SQL _sql = new SQL(); 

        /*public Action EveryoneOut()
        {
            var facultyInside = _sql.Faculties.Where(x => x.IsPresent == true);
            var studentsInside = _sql.Students.Where(x => x.IsPresent == true);

            foreach (var student in studentsInside)
            {
                student.IsPresent = false;
                _sql.Students.Update(student);
            }

            foreach (var faculty in facultyInside)
            {
                faculty.IsPresent = false;
                _sql.Faculties.Update(faculty);
            }

            _sql.SaveChanges();
        }

        public void Execute()
        {
            EveryoneOut();
        }*/
    }
}
