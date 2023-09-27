using System;
namespace miry.manage_personne.business.Dtos.PersonneDtos
{
	public class BasePersonneDto
	{
		public string Name { get; set; }
		public string Firstname { get; set; }

        public BasePersonneDto(string name, string firstname)
		{
			Name = name;
			Firstname = firstname;
		}
    }
}

