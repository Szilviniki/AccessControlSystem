namespace DB_Module.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required] [MaxLength(50)] public string Name { get; set; } = "";
        [MaxLength(100)] public string Description { get; set; } = "";

        public ICollection<Personnel> Users { get; } = new List<Personnel>();
    }
}
