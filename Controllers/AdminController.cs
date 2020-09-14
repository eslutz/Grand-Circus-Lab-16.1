using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_13._3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_13._3.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index(string delete="No", long productID=0, string edit="Cancel", string name="", string category="", string description="", string price="")
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
		public IActionResult New()
		{
			return View();
		}
	}
}
