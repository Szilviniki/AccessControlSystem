namespace DB_Module.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Personnel> Users { get; } = new List<Personnel>();
    }
}
