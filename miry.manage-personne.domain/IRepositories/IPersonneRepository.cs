using System;
using miry.manage_base.domain.IRepository;
using miry.manage_personne.domain.Models;

namespace miry.manage_personne.domain.IRepositories
{
	public interface IPersonneRepository: IBaseRepository<Personne>
	{
        IQueryable<Personne> FindBy(string? keyName, string? keyFirstname);
    }
}

