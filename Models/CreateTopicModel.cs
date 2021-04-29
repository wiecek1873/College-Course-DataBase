using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazadanych.Models
{
	public class CreateTopicModel
	{
		public TopicModel topicModel { get; set; }

		public IEnumerable<User> userModels { get; set; }
	}
}
