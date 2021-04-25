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
				decimal userID = 0;
				foreach (User user in modelContext.Users)
				{
					if (user.Emailadress == model.EmailAddress)
						return Error();
					userID++;
				}

				modelContext.Users.Add(new User
				{
					Userid = userID,
					Firstname = model.FirstName,
					Lastname = model.LastName,
					Emailadress = model.EmailAddress
				});

				modelContext.SaveChanges();
				modelContext.Dispose();
				return RedirectToAction("Index");
			}
			return View();
		}

		public ActionResult CreateTopic()
		{
			ViewBag.Message = "Create topic";

			return View();
		}

		[HttpPost]
		public ActionResult CreateTopic(TopicModel model)
		{
			if (ModelState.IsValid)
			{
				ModelContext modelContext = new ModelContext();
				decimal topicID = 0;
				decimal voteTimeID = 0;
				decimal optionID = 0;

				foreach(Votetime votetime in modelContext.Votetimes)
				{
					voteTimeID++;
				}

				modelContext.Votetimes.Add(new Votetime
				{
					Votetimeid = voteTimeID,
					Votestarttime = model.VoteStart,
					Votestoptime = model.VoteEnd
				});

				foreach(Option option in modelContext.Options)
				{
					optionID++;
				}

				modelContext.Options.Add(new Option
				{
					Optionid = optionID,
					Optiongroupid = 1, //TODO sparametryzować
					Information = "elo", //TODO sparametyzować
					Votes = 0
				});

				//foreach (Votetopic votetopic in modelContext.Votetopics)
				//{
				//	topicID++;
				//}

				//modelContext.Votetopics.Add(new Votetopic
				//{
				//	Votetopicid = topicID,
				//	Maininformation = model.Maininformation,
				//	Votetimeid = voteTimeID,
				//	Optiongroupid = optionGroupID
				//});

				modelContext.SaveChanges();
				modelContext.Dispose();
				return RedirectToAction("Index");
			}
			return View();
		}

		public ActionResult ViewTopics()
		{
			ModelContext modelContext = new ModelContext();
			ViewBag.Message = "Topics List";

			var data = modelContext.Votetopics.ToList();
			List<TopicModel> allTopics = new List<TopicModel>();

			foreach (var votetopic in data)
			{
				allTopics.Add(new TopicModel
				{
					VoteTopicID = (int)votetopic.Votetopicid,
					Maininformation = votetopic.Maininformation,
				});
			}

			allTopics = allTopics.OrderBy(x => x.VoteTopicID).ToList();

			return View(allTopics);
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
