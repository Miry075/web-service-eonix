using System;
namespace miry.manage_personne.business.Dtos.Filters
{
	public class PersonneFilterDto
	{
		public string KeyName{ get; set; }
        public string KeyFirstname { get; set; }

        public PersonneFilterDto(string keyName, string keyFirstname)
		{
			KeyName = keyName;
			KeyFirstname = keyFirstname;
		}
	}
}

