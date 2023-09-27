using System;
using miry.manage_base.service.IService;
using miry.manage_personne.business.Dtos;
using miry.manage_personne.business.Dtos.PersonneDtos;
using miry.manage_personne.domain.IRepositories;
using miry.manage_personne.domain.Models;

namespace miry.manage_personne.business.IServices
{
	public interface IPersonneService : IBaseService<Personne, PersonneReadDto, PersonneCreateDto, PersonneUpdateDto>
    {
        Task<IEnumerable<PersonneReadDto>> FindPersonnes(string? keyName, string? keyFirsName);
    }
}

