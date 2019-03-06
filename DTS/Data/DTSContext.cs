using Microsoft.EntityFrameworkCore;
using DTS.Models;

namespace DTS.Data
{
    public class DTSContext : DbContext
    {
        public DTSContext(DbContextOptions<DTSContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> Types { get; set; }
        public DbSet<UserStatus> Statuses { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateState> TemplateStates { get; set; }
        public DbSet<TemplateVersionControl> TemplateVersions { get; set; }
    }
}
