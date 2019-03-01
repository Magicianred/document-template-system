using Microsoft.EntityFrameworkCore;
using DTS.Models;

namespace DTS.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
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
