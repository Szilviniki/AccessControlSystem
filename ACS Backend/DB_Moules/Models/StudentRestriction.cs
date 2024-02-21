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
        [Required] [Column("student_id")] public Guid StudentId { get; set; }
        [Required] [Column("restriction_id")] public int RestrictionId { get; set; }
    }
}