namespace DB_Module.Models
{
    [Table("LockRules")]
    public class LockRule
    {
        [Key]
        public int LockRuleId { get; set; }

        [Required] [MaxLength(100)] public string Name { get; set; } = "";

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [Required]
        public int DayOfWeek { get; set; }

        public ICollection<StudentLockRule>? StudentLockRules { get; set; }
    }
}
