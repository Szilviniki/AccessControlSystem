namespace DB_Module.Models
{
    [Table("GateLogs")]
    [PrimaryKey("PersonId", "Stamp")]
    public class GateLog
    {
        [Column("cardId")] [Required] public int PersonId { get; set; }
        [Required] public DateTime Stamp { get; set; }
        public bool IsGuest { get; set; }
    }
}