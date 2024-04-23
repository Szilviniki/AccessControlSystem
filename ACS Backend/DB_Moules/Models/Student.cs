namespace DB_Module.Models
{
    
    [Table("Students")]
    public class Student
    {
        [Key]
        public Guid Id { get; set; }

        
        public int CardId {  get; set; }

        [Required] [MaxLength(50)] public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = "";

        
        public bool IsPresent { get; set; }

        [Required]
        [PhoneNumber]
        [MaxLength(13)]
        public string Phone { get; set; } = "";

        [Required]
        [BirthDate]
        public DateTime BirthDate { get; set; }

        [ForeignKey("ParentId")]
        public Guid ParentId { get; set; }
        
        public List<Note>? Notes { get; set; } = null!;
        
        public Guardian? Parent { get; set; } = null!;
    }
}
