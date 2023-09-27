using System;
namespace miry.manage_personne.business.Dtos.PersonneDtos
{
	public class PersonneReadDto: BasePersonneDto
	{
        public Guid Id { get; set; }
        public string FullName { get => $"{Firstname} {Name}"; }

        public PersonneReadDto(Guid id, string name, string firstname) : base(name, firstname)
        {
            Id = id;
        }
    }
}

