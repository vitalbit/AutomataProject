using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class EntityModel : DbContext
    {
        public EntityModel() : base("EntityModel")
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AttachmentContent> AttachmentContents { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<BlockType> BlockTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
