using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Module.Models
{
    [Table("StudentsRestrictions")]
    [PrimaryKey("StudentId", "RestrictionId")]
    public class StudentRestriction
    {
        [Required] [Column("studentId")] public Guid StudentId { get; set; }
        [Required] [Column("restrictionId")] public int RestrictionId { get; set; }
    }
}