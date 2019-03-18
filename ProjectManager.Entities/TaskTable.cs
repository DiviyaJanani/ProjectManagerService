using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace ProjectManager.Entities
{
    [Table("TaskTable")]
    public class TaskTable
    {
        [Key]
        public int TaskId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public int Priority { get; set; }
        public string ParentTask { get; set; }
        public bool IsParent { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Deleted { get; set; }
        public string Status { get; set; }

    }

    [Table("ProjectTable")]
    public class ProjectTable
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Priority { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public string ManagerName { get; set; }
        public bool Deleted { get; set; }
    }

    [Table("UserTable")]
    public class UserTable
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeId { get; set; }
        public bool Deleted { get; set; }
    }
}

