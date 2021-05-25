using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bazadanych.Models
{
	public class ResultModel
	{
		[Display(Name = "Description")]
		[Required(ErrorMessage = "Put some description here.")]
		[StringLength(2000, MinimumLength = 3, ErrorMessage = "Description should be longer than 3 characters and shorter than 2000")]
		public string Maininformation { get; set; }

		[Display(Name = "First option")]
		public OptionModel OptionA { get; set; }

		[Display(Name = "Second option")]
		public OptionModel OptionB { get; set; }
	}
}
