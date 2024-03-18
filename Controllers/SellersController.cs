using Microsoft.AspNetCore.Mvc;
using SallesWebApp.Models;
using SallesWebApp.Models.ViewModels;
using SallesWebApp.Services;
using SallesWebApp.Services.Exceptions;
using System.Diagnostics;

namespace SallesWebApp.Controllers
{
	public class SellersController : Controller
	{
		private readonly SellerService _sellerService;
		private readonly DepartmentService _departmentService;

		public SellersController(SellerService sellerService, DepartmentService departmentService)
		{
			_sellerService = sellerService;
			_departmentService = departmentService;
		}

		public IActionResult Index()
		{
			var list = _sellerService.FindAll();
			return View(list);
		}

		public IActionResult Create()
		{
			var departments = _departmentService.FindAll();
			var viewModel = new SellerFormViewModel(departments);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Seller seller)
		{
			if (!ModelState.IsValid)
			{
				var departments = _departmentService.FindAll();
				var viewModel = new SellerFormViewModel(seller, departments);
				return View(viewModel);
			}

			_sellerService.Insert(seller);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Insert a valid id, please!" });
			}

			var seller = _sellerService.FindById(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Seller not found!" });
			}

			return View(seller);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			_sellerService.Remove(id);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Insert a valid id, please!" });
			}

			var seller = _sellerService.FindById(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Seller not found!" });

			}

			return View(seller);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Insert a valid id, please!" });
			}
			var seller = _sellerService.FindById(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Seller not found!" });

			}

			List<Department> departments = _departmentService.FindAll();
			SellerFormViewModel viewModel = new SellerFormViewModel(seller, departments);

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Seller seller)
		{
			if (!ModelState.IsValid)
			{
				var departments = _departmentService.FindAll();
				var viewModel = new SellerFormViewModel(seller, departments);
				return View(viewModel);
			}

			if (id != seller.Id)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id mismatch!" });
			}

			try
			{
				_sellerService.Update(seller);
				return RedirectToAction(nameof(Index));
			}
			catch (NotFoundException ex)
			{
				return RedirectToAction(nameof(Error), new { ex.Message });
			}
			catch (DbConcurrencyException ex)
			{
				return RedirectToAction(nameof(Error), new { ex.Message });
			}
		}

		public IActionResult Error(string message)
		{
			var viewError = new ErrorViewModel
			{
				Message = message,
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			};

			return View(viewError);
		}
	}
}
