using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using miry.manage_base.service.IService;
using miry.manage_base.service.Service;
using miry.manage_personne.business.Dtos;
using miry.manage_personne.business.Dtos.PersonneDtos;
using miry.manage_personne.business.IServices;
using miry.manage_personne.domain.IRepositories;
using miry.manage_personne.domain.Models;
namespace miry.manage_personne.business.Services
{
	public class PersonneService: BaseService<Personne, PersonneReadDto, PersonneCreateDto, PersonneUpdateDto, IPersonneRepository>, IPersonneService
    {
        public PersonneService(IPersonneRepository personneRepository, IMapper mapper): base(personneRepository, mapper)
		{
        }

        public async Task<IEnumerable<PersonneReadDto>> FindPersonnes(string? keyName, string? keyFirsname)
        {
            try
            {
                var entities = await _repository.FindBy(keyName, keyFirsname).ToListAsync();
                return _mapper.Map<IEnumerable<PersonneReadDto>>(entities);
            }
            catch { throw; }

        }

    }
}

