namespace DB_Module.Models
{
    [Table("Restrictions")]
    public class Restriction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
