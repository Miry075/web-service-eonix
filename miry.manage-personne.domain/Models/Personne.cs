using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using miry.manage_base.domain.Models;

namespace miry.manage_personne.domain.Models
{
    public class Personne: BaseEntity
	{

        [Required]
        public string Name { get; set; }

        [Required]
        public string Firstname { get; set; }


		public Personne(Guid id, string name, string firstname)
		{
			Id = id;
			Name = name;
			Firstname = firstname;
		}

        public Personne( string name, string firstname)
        {
            Name = name;
            Firstname = firstname;
        }
    }
}

