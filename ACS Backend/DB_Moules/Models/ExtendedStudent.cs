using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Module.Models
{
    [Keyless]
    public class ExtendedStudent
    {
        public string StudentName { get; }
        public Guid StudentId { get; }
        public string StudentEmail { get; }
        public string StudentPhone { get; }
        public int GroupId {  get; }
        public string GroupName { get; }
        public int CardId { get; }
        public DateTime BirthDate { get; }
        public string ParentName { get; }
        public string ParentPhone { get; }
        public string ParentEmail { get; }
        public bool isPresent { get; }
    }
}
