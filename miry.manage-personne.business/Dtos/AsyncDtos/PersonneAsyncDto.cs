using System;
namespace miry.manage_personne.business.Dtos.AsyncDtos
{
	public class PersonneAsyncDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }

		public string Event { get; set; }

        public PersonneAsyncDto(Guid id, string name, string firstname)
		{
			Id = id;
			Name = name;
			Firstname = firstname;
		}
	}
}

