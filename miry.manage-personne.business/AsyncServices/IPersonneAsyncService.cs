using System;
using miry.async_service.client.AsyncServices;
using miry.manage_personne.business.Dtos.AsyncDtos;
using miry.manage_personne.business.Dtos.PersonneDtos;

namespace miry.manage_personne.business.AsyncServices
{
	public interface IPersonneAsyncService : IAsyncService<PersonneAsyncDto>
    {
        void PublishData(PersonneReadDto readDto, string eventName);
    }
}

