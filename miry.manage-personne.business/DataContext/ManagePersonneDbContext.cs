using System;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using miry.manage_personne.domain.Models;

namespace miry.manage_personne.business.DataContext
{
	public class ManagePersonneDbContext: DbContext
	{
		public ManagePersonneDbContext(DbContextOptions<ManagePersonneDbContext> options):base(options)
		{
			Database.EnsureCreated();
		}

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
        }

        public DbSet<Personne> Personnes { get; set; }
    }
}

