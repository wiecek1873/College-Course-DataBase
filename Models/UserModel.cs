using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bazadanych.Models
{
	public class UserModel
	{
		public int UserID { get; set; }

		[Display(Name = "First Name")]
		[Required(ErrorMessage = "We need your first name.")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		[Required(ErrorMessage = "We need your last name.")]
		public string LastName { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email Address")]
		[Required(ErrorMessage = "We need your email address.")]
		public string EmailAddress { get; set; }
	}
}
