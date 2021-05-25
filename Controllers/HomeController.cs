using Bazadanych.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using System.Web;

namespace Bazadanych.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private int sessionUserId = -1;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ViewUsers()
		{
			ModelContext modelContext = new ModelContext();

			var allUsersDB = modelContext.Users.ToList();

			List<UserModel> allUsers = new List<UserModel>();

			foreach (User user in allUsersDB)
			{
				allUsers.Add(new UserModel
				{
					UserID = (int)user.Userid,
					FirstName = user.Firstname,
					LastName = user.Lastname,
					EmailAddress = user.Emailadress
				});
			}

			allUsers = allUsers.OrderBy(x => x.UserID).ToList();

			modelContext.Dispose();
			return View(allUsers);
		}

		public IActionResult SignIn(int id)
		{
			HttpContext.Session.Set("sessionUserId",BitConverter.GetBytes(id));
			return RedirectToAction("Index");
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
			CreateTopicModel createTopicModel = new CreateTopicModel();
			createTopicModel.topicModel = new TopicModel();

			return View(createTopicModel);
		}

		[HttpPost]
		public ActionResult CreateTopic(CreateTopicModel model)
		{
			if (ModelState.IsValid)
			{
				ModelContext modelContext = new ModelContext();
				decimal topicID = 0;
				decimal voteTimeID;
				decimal optionID = 0;

				foreach (Votetopic votetopic in modelContext.Votetopics)
				{
					topicID++;
				}

				voteTimeID = topicID;

				modelContext.Votetimes.Add(new Votetime
				{
					Votetimeid = voteTimeID,
					Votestarttime = model.topicModel.VoteStart,
					Votestoptime = model.topicModel.VoteEnd
				});

				foreach (Option option in modelContext.Options)
				{
					optionID++;
				}

				modelContext.Options.Add(new Option
				{
					Optionid = optionID,
					Optiongroupid = topicID,
					Information = model.topicModel.OptionA.Information,
					Votes = 0
				});

				optionID++;
				modelContext.Options.Add(new Option
				{
					Optionid = optionID,
					Optiongroupid = topicID,
					Information = model.topicModel.OptionB.Information,
					Votes = 0
				});

				modelContext.Votetopics.Add(new Votetopic
				{
					Votetopicid = topicID,
					Maininformation = model.topicModel.Maininformation,
					Votetimeid = topicID,
					Optiongroupid = topicID
				});

				List<string> users = model.users.Split(' ').ToList();
				List<User> allUsersDB = modelContext.Users.ToList();

				decimal permissionID = 0;
				foreach (var permission in modelContext.Permissions.ToList())
				{
					permissionID++;
				}

				foreach (var user in users)
				{
					foreach (var userDB in allUsersDB)
					{
						if (user == userDB.Emailadress)
						{
							modelContext.Permissions.Add(new Permission
							{
								Permissionid = permissionID,
								Usersid = userDB.Userid,
								Topicsid = topicID,
								Canvote = 1
							});
							permissionID++;
						}
					}
				}

				modelContext.SaveChanges();
				modelContext.Dispose();
				return RedirectToAction("ViewTopics");
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
			List<TopicModel> topicToView = new List<TopicModel>();

			HttpContext.Session.TryGetValue("sessionUserId", out Byte[] bytes);
			if (bytes != null)
			{
				sessionUserId = BitConverter.ToInt32(bytes);

				foreach (var permission in modelContext.Permissions)
				{
					for (int i = 0; i < allTopics.Count; i++)
					{
						if (permission.Usersid == sessionUserId && permission.Topicsid == allTopics[i].VoteTopicID && permission.Canvote == 1 && DateTime.Compare(allTopics[i].VoteStart,DateTime.Now) < 0)
							topicToView.Add(allTopics[i]);
					}
				}
			}
			else
				topicToView = allTopics;

			modelContext.Dispose();
			return View(topicToView);
		}

		public ActionResult ViewAllTopics()
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
			List<TopicModel> topicToView = new List<TopicModel>();

			HttpContext.Session.TryGetValue("sessionUserId", out Byte[] bytes);
			if (bytes != null)
			{
				sessionUserId = BitConverter.ToInt32(bytes);

				foreach (var permission in modelContext.Permissions)
				{
					for (int i = 0; i < allTopics.Count; i++)
					{
						if (permission.Usersid == sessionUserId && permission.Topicsid == allTopics[i].VoteTopicID)
							topicToView.Add(allTopics[i]);
					}
				}
			}
			else
				topicToView = allTopics;

			modelContext.Dispose();
			return View(topicToView);
		}

		public ActionResult ViewResults()
		{
			ModelContext modelContext = new ModelContext();
			var allTopicsDB = modelContext.Votetopics.ToList();
			var allTimesDB = modelContext.Votetimes.ToList();
			var allOptionsDB = modelContext.Options.ToList();
			List<ResultModel> resultModels = new List<ResultModel>();

			HttpContext.Session.TryGetValue("sessionUserId", out Byte[] bytes);
			if(bytes != null)
			{
				sessionUserId = BitConverter.ToInt32(bytes);
				foreach(var permission in modelContext.Permissions.ToList())
				{
					if(permission.Usersid == sessionUserId)
					{
						foreach(var votetime in allTimesDB)
						{
							if(permission.Topicsid == votetime.Votetimeid)
							{
								if(DateTime.Compare(votetime.Votestoptime,DateTime.Now) < 0)
								{
									var model = new ResultModel();
									foreach(var topic in allTopicsDB)
									{
										if(topic.Votetopicid == permission.Topicsid)
										{
											model.Maininformation = topic.Maininformation;
										}
									}
									foreach(var option in allOptionsDB)
									{
										if(option.Optiongroupid == permission.Topicsid)
										{
											if(model.OptionA == null)
											{
												model.OptionA = new OptionModel
												{
													Information = option.Information,
													Votes = (int)option.Votes
												};
											}
											else
											{
												model.OptionB = new OptionModel
												{
													Information = option.Information,
													Votes = (int)option.Votes
												};
											}
										}
									}
									resultModels.Add(model);
								}
							}
						}
					}
				}
			}

			return View(resultModels);
		}

		[HttpPost]
		public ActionResult VoteTopic(string submit, int id)
		{
			ModelContext modelContext = new ModelContext();

			HttpContext.Session.TryGetValue("sessionUserId", out Byte[] bytes);
			sessionUserId = BitConverter.ToInt32(bytes);

			var options = modelContext.Options.ToList().FindAll(x => x.Optiongroupid == id);
			options = options.OrderBy(x => x.Optionid).ToList();
			if (submit == "Vote!")
			{
				options.First().Votes++;
			}
			else
			{
				options.Last().Votes++;
			}

			foreach(var permission in modelContext.Permissions)
			{
				if(permission.Usersid == sessionUserId && permission.Topicsid == id)
				{
					permission.Canvote = 0;
				}
			}

			modelContext.SaveChanges();
			modelContext.Dispose();

			return RedirectToAction("ViewTopics");
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

		public ActionResult NoPermission()
		{
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
