namespace DB_Module.Models
{
    [Table("Parents")]
    public class Guardian
    {
        [Key] public Guid Id { get; set; }

        [Required] 
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [Required]
        [PhoneNumber]
        [MaxLength(13)]
        public string Phone { get; set; } = "";

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = "";
    }
}