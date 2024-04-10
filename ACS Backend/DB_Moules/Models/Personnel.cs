namespace DB_Module.Models
{
    [Table("Personnel")]
    public class Personnel
    {
        [Key] [Required] public Guid Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public int CardId { get; set; }

        [EmailAddress]
        public string  Email { get; set; } //unique

        public string Password { get; set; }

        [PhoneNumber]
        public string Phone { get; set; }
        [Column("present")] public bool IsPresent { get; set; }

        [ForeignKey("RoleId")] public int RoleId { get; set; }

        public bool CanLogin { get; set; }
    }
}