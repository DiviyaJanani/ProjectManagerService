using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Entities;
using System.Data.Entity;

namespace ProjectManager.DL
{
    public class DataContext : DbContext
    {
        
        public DataContext() : base("name = ProjectManagerConnectionString")
        {
        }
        public DbSet<TaskTable> Tasks { get; set; }
        public DbSet<ProjectTable> Projects { get; set; }
        public DbSet<UserTable> Users { get; set; }
    }
}
