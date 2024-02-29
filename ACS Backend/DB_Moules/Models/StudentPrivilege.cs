using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Module.Models
{
    [Table("StudentsPrivileges")]
    [PrimaryKey("StudentId", "PrivilegeId")]
    public class StudentPrivilege
    {
        [Required] [Column("studentId")] public Guid StudentId { get; set; }
        [Required] [Column("privilegeId")] public int PrivilegeId { get; set; }
    }
}