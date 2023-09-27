using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using miry.manage_personne.business.Dtos.AsyncDtos;
using miry.manage_personne.business.Dtos.PersonneDtos;
using miry.manage_personne.domain.Models;

namespace miry.manage_personne.business.MappingProfiles
{
	public class PersonneProfile:Profile
	{
		public PersonneProfile()
		{
            CreateMap<Personne, PersonneReadDto>();
            CreateMap<PersonneCreateDto, Personne>();
            CreateMap<PersonneReadDto, PersonneAsyncDto>();
        }
	}
}

