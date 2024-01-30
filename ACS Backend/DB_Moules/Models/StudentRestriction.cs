using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Module.Models
{
    [Table("StudentRestrictions")]

    public class StudentRestriction
    {
        [Required]
        [Key]
        public Guid StudentId { get; set; }
        [Key]
        [Required]
        public int RestrictionId { get; set; }
    }
}
