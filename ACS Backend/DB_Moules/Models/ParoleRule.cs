namespace DB_Module.Models
{
    [Table("ParoleRules")]
    public class ParoleRule
    {
        [Key] public int Id { get; set; }

        [Required] [MaxLength(100)] public string Name { get; set; } = "";


        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [Required] public int DayOfWeek { get; set; }

    }
}