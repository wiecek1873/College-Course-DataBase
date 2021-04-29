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

				foreach (Votetopic votetopic in modelContext.Votetopics)
				{
					topicID++;
				}

				foreach (Votetime votetime in modelContext.Votetimes)
				{
					voteTimeID++;
				}

				modelContext.Votetimes.Add(new Votetime
				{
					Votetimeid = voteTimeID,
					Votestarttime = model.VoteStart,
					Votestoptime = model.VoteEnd
				});

				foreach (Option option in modelContext.Options)
				{
					optionID++;
				}

				modelContext.Options.Add(new Option
				{
					Optionid = optionID,
					Optiongroupid = topicID, //TODO sparametryzować
					Information = model.OptionA.Information, //TODO sparametyzować
					Votes = 0
				});

				optionID++;
				modelContext.Options.Add(new Option
				{
					Optionid = optionID,
					Optiongroupid = topicID, //TODO sparametryzować
					Information = model.OptionB.Information, //TODO sparametyzować
					Votes = 0
				});

				modelContext.Votetopics.Add(new Votetopic
				{
					Votetopicid = topicID,
					Maininformation = model.Maininformation,
					Votetimeid = voteTimeID,
					Optiongroupid = topicID
				});

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

			var allTopicsDB = modelContext.Votetopics.ToList();
			var allTimesDB = modelContext.Votetimes.ToList();
			var allOptionsDB = modelContext.Options.ToList();
			List<TopicModel> allTopics = new List<TopicModel>();

			foreach (var votetopic in allTopicsDB)
			{
				List<Option> options = allOptionsDB.FindAll(x => x.Optiongroupid == votetopic.Optiongroupid).ToList();
				options = options.OrderBy(x => x.Optionid).ToList();
				OptionModel optionA = new OptionModel();
				OptionModel optionB = new OptionModel();

				optionA.Information = options.First().Information;
				optionB.Information = options.Last().Information;

				allTopics.Add(new TopicModel
				{
					VoteTopicID = (int)votetopic.Votetopicid,
					Maininformation = votetopic.Maininformation,
					VoteStart = allTimesDB.Find(x => x.Votetimeid == votetopic.Votetimeid).Votestarttime,
					VoteEnd = allTimesDB.Find(x => x.Votetimeid == votetopic.Votetimeid).Votestoptime,
					OptionA = optionA,
					OptionB = optionB
				});
			}

			allTopics = allTopics.OrderBy(x => x.VoteTopicID).ToList();

			modelContext.Dispose();
			return View(allTopics);
		}

		[HttpPost]
		public ActionResult VoteTopic(string submit, int id)
		{
			ModelContext modelContext = new ModelContext();

			var options = modelContext.Options.ToList().FindAll(x => x.Optiongroupid == id);
			options = options.OrderBy(x => x.Optionid).ToList();
			if (submit == "OptionA")
			{
				options.First().Votes++;
			}
			else
			{
				options.Last().Votes++;
			}

			modelContext.SaveChanges();
			modelContext.Dispose();

			return View();
		}


		public ActionResult VoteTopic(int id)
		{
			ViewBag.Message = "Vote on topic";

			ModelContext modelContext = new ModelContext();

			var allTopicsDB = modelContext.Votetopics.ToList();
			var allTimesDB = modelContext.Votetimes.ToList();
			var allOptionsDB = modelContext.Options.ToList();

			var topic = allTopicsDB.Find(x => x.Votetopicid == id);

			List<Option> options = allOptionsDB.FindAll(x => x.Optiongroupid == topic.Optiongroupid).ToList();
			options = options.OrderBy(x => x.Optionid).ToList();
			OptionModel optionA = new OptionModel();
			OptionModel optionB = new OptionModel();

			optionA.Information = options.First().Information;
			optionB.Information = options.Last().Information;

			TopicModel topicModel = new TopicModel
			{
				VoteTopicID = (int)topic.Votetopicid,
				Maininformation = topic.Maininformation,
				VoteStart = allTimesDB.Find(x => x.Votetimeid == topic.Votetimeid).Votestarttime,
				VoteEnd = allTimesDB.Find(x => x.Votetimeid == topic.Votetimeid).Votestoptime,
				OptionA = optionA,
				OptionB = optionB
			};

			return View(topicModel);
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
