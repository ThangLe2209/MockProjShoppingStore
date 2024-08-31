using ShoppingStore.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Model.Dtos
{
	public class OrderForCreationDto
	{
		public string OrderCode { get; set; }
		public string UserName { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
	}
}
