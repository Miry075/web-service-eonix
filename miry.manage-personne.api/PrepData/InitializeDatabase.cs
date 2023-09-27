using System;
using miry.manage_personne.business.DataContext;
using miry.manage_personne.domain.Models;

namespace miry.manage_personne.api.PrepData
{
    public static class InitializeDatabase
    {
        public static void PopulateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                PopulatePlatformData(serviceScope.ServiceProvider.GetService<ManagePersonneDbContext>());
            }
        }

        private static void PopulatePlatformData(ManagePersonneDbContext? context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var personneDbSet = context.Set<Personne>();
            if (!personneDbSet.Any())
            {
                personneDbSet.AddRange(
                    new Personne("Alex", "Smith"),
                    new Personne("Karly", "Dotson"),
                    new Personne("Jamalia", "Dunlap"),
                    new Personne("Byron", "Stark"),
                    new Personne("Micah", "Conner"),
                    new Personne("Catherine", "Best"),
                    new Personne("Riley", "Goodman"),
                    new Personne("Burton", "Mcmahon"),
                    new Personne("Guy", "Chandler"),
                    new Personne("Fiona", "Crawford")
                );
                context.SaveChanges();
            }
        }
    }
}

