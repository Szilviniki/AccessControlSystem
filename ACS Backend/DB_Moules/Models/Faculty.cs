namespace DB_Module.Models
{
    [Table("Faculty")]
    public class Faculty
    {
        [Key] [Required] public Guid Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public int CardId { get; set; }

        [Required] public string Email { get; set; } //unique

        [Required] public string Password { get; set; }

        public string Phone { get; set; }

        public bool Present { get; set; }

        public int RoleId { get; set; }

    }
}