namespace DB_Module.Models
{
    [Table("Personnel")]
    public class Personnel
    {
        [Key] public Guid Id { get; set; }

        [Required] [MaxLength(50)] public string Name { get; set; } = "";

        [Required] public int CardId { get; set; }

        [EmailAddress] [MaxLength(100)] public string Email { get; set; } = "";

        [MaxLength(255)] public string Password { get; set; } = "";

        [PhoneNumber] [MaxLength(13)] public string Phone { get; set; } = "";
        [Column("present")] public bool IsPresent { get; set; }

        [ForeignKey("RoleId")] public int RoleId { get; set; }
        
        public Role? Role { get; set; }

        public bool CanLogin { get; set; }
    }
}