using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bazadanych.Models
{
	public class TopicModel
	{
		public int VoteTopicID { get; set; }

		[Display(Name = "Description")]
		[Required(ErrorMessage = "Put some description here.")]
		[StringLength(2000,MinimumLength = 3,ErrorMessage = "Description should be longer than 3 characters and shorter than 2000")]
		public string Maininformation { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Start")]
		[Required(ErrorMessage = "Need time to start.")]
		public DateTime VoteStart { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Finish")]
		[Required(ErrorMessage = "Need time to stop.")]
		public DateTime VoteEnd { get; set; }

		public OptionModel Option { get; set; }
	}
}
