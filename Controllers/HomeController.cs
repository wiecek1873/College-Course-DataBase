using Bazadanych.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bazadanych.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult SignUp()
		{
			ViewBag.Message = "User sign up";

			return View();
		}

		[HttpPost]
		public IActionResult SignUp(UserModel model)
		{
			if (ModelState.IsValid)
			{
				ModelContext modelContext = new ModelContext();
				User newUser = new User();
				newUser.Userid = 1;
				newUser.Firstname = model.FirstName;
				newUser.Lastname = model.LastName;
				newUser.Emailadress = model.EmailAddress;
				modelContext.Users.Add(newUser);
				modelContext.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}

		//public string Test()
		//{
		//	ModelContext xd = new ModelContext();
		//	var xdd = xd.Users.Where(x => x.Userid == 0).First().Userid.ToString();
		//	return xdd;
		//}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
