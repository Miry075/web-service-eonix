using System;
using miry.manage_personne.business.Dtos.PersonneDtos;

namespace miry.manage_personne.business.Dtos
{
	public class PersonneUpdateDto: BasePersonneDto
	{
        public Guid Id { get; set; }
        public PersonneUpdateDto(Guid id, string name, string firstname) : base(name, firstname)
        {
            Id = id;
        }
    }
}

