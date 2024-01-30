namespace DB_Module.Models
{
    [Table("Privileges")]
    public class Privilege
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
