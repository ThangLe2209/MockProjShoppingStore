using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Model.Dtos
{
    public class RatingForCreationDto
	{
        public Guid ProductId { get; set; }
        public string Comment { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Star { get; set; }
    }
}
