using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectASP.Models
{
    public class MemberContext: DbContext
    {
        public MemberContext(DbContextOptions<MemberContext>options)
            : base(options)
        {

        }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollGebruiker> PollGebruikers { get; set; }

        public DbSet<Stem> Stems { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker");
            modelBuilder.Entity<Stem>().ToTable("Stem");
            modelBuilder.Entity<Option>().ToTable("Option");
        }
    }
}
