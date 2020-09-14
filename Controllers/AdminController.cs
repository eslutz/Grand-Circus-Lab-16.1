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

		public IActionResult Index(string delete="No", long productID=0)
		{
			if(delete == "Yes")
			{
				Product.Delete(productID);
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
	}
}
