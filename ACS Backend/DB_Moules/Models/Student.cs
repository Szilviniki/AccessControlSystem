using DB_Module.Attributes;

namespace DB_Module.Models
{
    
    [Table("Students")]
    public class Student
    {
        [Key]
        public Guid Id { get; set; }

        
        public int CardId {  get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        
        public bool IsPresent { get; set; }

        [Required]
        [PhoneNumber]
        public string Phone { get; set; }

        [Required]
        [BirthDate]
        public DateTime BirthDate { get; set; }

        [ForeignKey("ParentId")]
        public Guid ParentId { get; set; }


        public Guardian? Parent { get; set; } = null!;
    }
}
