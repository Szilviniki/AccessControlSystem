namespace DB_Module.Models
{
    [Table("Parents")]
    public class Guardian
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
