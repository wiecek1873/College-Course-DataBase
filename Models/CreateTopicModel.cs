using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bazadanych.Models
{
	public class CreateTopicModel
	{
		public TopicModel topicModel { get; set; }

		[Display(Name = "User's emails")]
		public string users { get; set; }
	}
}
