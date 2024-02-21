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
        [Required] [Column("student_id")] public Guid StudentId { get; set; }
        [Required] [Column("privilege_id")] public int PrivilegeId { get; set; }
    }
}