using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lab_13._3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_13._3.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index(string delete="No", long productID=0, string edit="", string name="", string category="", string description="", string price="")
		{
			if(delete == "Yes")
			{
				Product.Delete(productID);
			}
			if(edit == "Save")
			{
				Product productUpdate = Product.Read(productID);
				productUpdate.Name = name;
				productUpdate.Category = category;
				productUpdate.Description = description;
				productUpdate.Price = decimal.Parse(price);
				productUpdate.Save();
			}
			return View(Product.Read());
		}

		[HttpPost]
		public IActionResult Edit(long productID)
		{
			return View(Product.Read(productID));
		}

		[HttpPost]
		public IActionResult Delete(long productID)
		{
			return View(Product.Read(productID));
		}

		[HttpPost]
		public IActionResult New(string name, string category, string description, string price)
		{
			Regex validString = new Regex(@"^[A-Z]([A-Za-z]|\s){0,29}$");
			Regex validPrice = new Regex(@"^[1-9][0-9]?(\.[0-9]{1,2})?$");
			bool validUserInput = true;
			string validInput = "";

			if(!String.IsNullOrEmpty(name) && !(validString.IsMatch(name) && validString.IsMatch(category) && validString.IsMatch(description)))
			{
				validInput = "Invalid Name, Cateogry, or Description.<br />Please enter only letters.";
				validUserInput = false;
			}
			else if(!String.IsNullOrEmpty(name) && !validPrice.IsMatch(price))
			{
				validInput = "Invalid Price.<br />Please enter a price of $99.99 or less.";
				validUserInput = false;
			}

			if (!validUserInput)
			{
				ViewBag.Message = validInput;
				return View();
			}
			else if (String.IsNullOrEmpty(name))
			{
				ViewBag.Message = "";
				return View();
			}
			else
			{
				Product newProduct = Product.Create(name, category, description, decimal.Parse(price));
				ViewBag.Message = "Product successfully added!";
				return View(newProduct);
			}
		}
	}
}
