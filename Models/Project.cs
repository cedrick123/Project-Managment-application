using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace new_project.Models
{
    public class Project
    {
        [Key]
        public int projectId { set; get; }
        public string name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal budget { get; set; }
        public DateTime dateOfStart { get; set; }
        public DateTime dateOfFinish { get; set; }
        public ICollection<User> workers { get; set; }

    }
}