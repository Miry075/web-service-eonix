using System;
namespace miry.manage_personne.business.Dtos.PersonneDtos
{
	public class PersonneCreateDto: BasePersonneDto
    {
		public PersonneCreateDto(string name, string firstname):base(name, firstname)
		{
		}
	}
}

