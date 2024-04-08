namespace ACS_Backend.Model
{
    public class AddStudentWithParentModel
    {
        public string StudentName { get; set; }

        public string StudentEmail { get; set; }
        
        public string StudentPhone { get; set;}
        
        public DateTime StudentBirthDate { get; set; }

        public Guid ParentId { get; set; }

        public string ParentName { get; set; }

        public string ParentEmail { get; set; }

        public string ParentPhone { get; set; }

    }
}
