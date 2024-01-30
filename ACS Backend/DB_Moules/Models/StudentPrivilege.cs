using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Module.Models
{
    [Table("StudentPrivileges")]
    [PrimaryKey("StudentId", "PrivilegeId")]
    public class StudentPrivilege
    {
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public int PrivilegeId { get; set; }
    }
}
