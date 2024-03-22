namespace DB_Module.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        
        public int CardId {  get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        
        public bool IsPresent { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [ForeignKey("ParentId")]
        public Guid ParentId { get; set; }
    }
}
