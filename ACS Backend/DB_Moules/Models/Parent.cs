namespace DB_Module.Models
{

    public class Parent
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Email { get; set; }

        [Required]
        public bool IsPrimary { get; set; } = false;
    }
}
