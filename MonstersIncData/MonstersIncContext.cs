using Microsoft.EntityFrameworkCore;
using MonstersIncDomain;
using System;

namespace MonstersIncData
{
    public class MonstersIncContext : DbContext
    {
        public DbSet<Intimidator> Intimidators { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<IntimidatorIntimidation> IntimidatorIntimidations { get; set; }
        public DbSet<Log> Logs { get; set; }
        public MonstersIncContext()
        {
        }

        public MonstersIncContext(DbContextOptions<MonstersIncContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //    //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
                //    //base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
