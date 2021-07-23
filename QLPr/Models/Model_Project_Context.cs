namespace QLPr.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model_Project_Context : DbContext
    {
        public Model_Project_Context()
            : base("name=Model_Project_Context")
        {
        }

        public System.Data.Entity.DbSet<QLPr.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<QLPr.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<QLPr.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<QLPr.Models.AssignTask> AssignTasks { get; set; }
    }
}
