using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab_13._3.Models;
using System.Text.RegularExpressions;

namespace Lab_13._3.Controllers
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

		[HttpGet]
		public IActionResult Register()
		{
			ViewBag.Message = "Welcome to the registration page.";

			return View();
		}

		[HttpPost]
		public IActionResult Registered(string firstname, string lastname, string gender, DateTime birthday, string email, string phonenumber, string state, string password, string repassword)
		{
			Regex validName = new Regex(@"^[A-Z][A-Za-z]{0,29}$");
			Regex validEmail = new Regex(@"^.{5,30}@\w{5,10}\.\w{2,3}$");
			Regex validPhonenumber = new Regex(@"^\({0,1}\d{3}\){0,1}[-.]\d{3}[-.]\d{4}$");
			bool passwordMatch = (password == repassword);
			bool allgood = passwordMatch && (validName.IsMatch(firstname) && validName.IsMatch(lastname) && validEmail.IsMatch(email) && validPhonenumber.IsMatch(phonenumber));

			if (allgood)
			{
				TheUser user = new TheUser()
				{
					FirstName = firstname,
					LastName = lastname,
					Gender = gender,
					Birthday = birthday,
					Email = email,
					PhoneNumber = phonenumber,
					State = state,
					Password = password
				};

				ViewBag.ValidRegister = "You have <b>sucessfully</b> registered!";
				return View(user);
			}
			else
			{
				List<string> invalidInputs = new List<string>();
				string invalidInputResults = "";
				if (!(validName.IsMatch(firstname) && validName.IsMatch(lastname)))
				{
					invalidInputs.Add("name");
				}
				if (!validEmail.IsMatch(email))
				{
					invalidInputs.Add("email");
				}
				if (!validPhonenumber.IsMatch(phonenumber))
				{
					invalidInputs.Add("phone number");
				}
				if (!passwordMatch)
				{
					invalidInputs.Add("passwords");
				}

				if (invalidInputs.Count == 1)
				{
					invalidInputResults += invalidInputs[0];
				}
				else if (invalidInputs.Count == 2)
				{
					invalidInputResults += $"{invalidInputs[0]} and {invalidInputs[1]}";
				}
				else if (invalidInputs.Count == 3)
				{
					invalidInputResults += $"{invalidInputs[0]}, {invalidInputs[1]}, and {invalidInputs[2]}";
				}
				else if (invalidInputs.Count == 4)
				{
					invalidInputResults += $"{invalidInputs[0]}, {invalidInputs[1]}, {invalidInputs[2]}, and {invalidInputs[3]}";
				}
				ViewBag.ValidRegister = "You have <b>FAILED</b> to sucessfully register!";
				ViewBag.Message = $"You incorrectly entered your {invalidInputResults}.<br />Please go back and <a href=\"/Home/Register\">try again!</a>";
				return View();
			}
		}

		public IActionResult TermsAndConditions()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
