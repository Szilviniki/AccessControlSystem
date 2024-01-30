﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Module.Models
{
    [Table("StudentParents")]
    [PrimaryKey("StudentId", "ParentId")]
    public class StudentParent
    {
        [Required]
        public Guid ParentId { get; set; }

        [Required]
        public Guid StudentId { get; set; }
    }
}
