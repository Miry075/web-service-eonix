using System;
using Microsoft.EntityFrameworkCore;
using miry.manage_base.repository.Repository;
using miry.manage_personne.business.DataContext;
using miry.manage_personne.domain.IRepositories;
using miry.manage_personne.domain.Models;

namespace miry.manage_personne.business.Repositories
{
	public class PersonneRepository: BaseRepository<Personne>, IPersonneRepository
	{
		public PersonneRepository(ManagePersonneDbContext context): base(context)
		{
		}

        public IQueryable<Personne> FindBy(string keyName, string keyFirstname)
        {
            var results = GetEntities(null);
            if (keyName != null)
            {
                results = results.Where(entity => entity.Name.ToLower().StartsWith(keyName.ToLower()) || entity.Name.ToLower().EndsWith(keyName.ToLower()));
            }
            if (keyFirstname != null)
            {
                results = results.Where(entity => entity.Firstname.ToLower().StartsWith(keyFirstname.ToLower()) || entity.Firstname.EndsWith(keyFirstname.ToLower()));
            }
            return results;
        }
       
    }
}

