using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bazadanych.Models
{
	public class OptionModel
	{
		public int OptionID { get; set; }

		public int OptionGroupID { get; set; }

		public string Information { get; set; }

		public int Votes { get; set; }
	}
}
