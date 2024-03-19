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

		public async Task<IActionResult> Index()
		{
			var list = await _sellerService.FindAllAsync();
			return View(list);
		}

		public async Task<IActionResult> Create()
		{
			var departments = await _departmentService.FindAllAsync();
			var viewModel = new SellerFormViewModel { Departments = departments };
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Seller seller)
		{
			if (!ModelState.IsValid)
			{
				var departments = await _departmentService.FindAllAsync();
				var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
				return View(viewModel);
			}

			await _sellerService.InsertAsync(seller);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Insert a valid id, please!" });
			}

			var seller = await _sellerService.FindByIdAsync(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Seller not found!" });
			}

			return View(seller);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _sellerService.RemoveAsync(id);
				return RedirectToAction(nameof(Index));
			}
			catch(IntegrityException ex) 
			{
				return RedirectToAction(nameof(Error), new { Message = ex.Message });
			}
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Insert a valid id, please!" });
			}

			var seller = await _sellerService.FindByIdAsync(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Seller not found!" });

			}

			return View(seller);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Insert a valid id, please!" });
			}
			var seller = await _sellerService.FindByIdAsync(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Seller not found!" });

			}

			List<Department> departments = await _departmentService.FindAllAsync();
			SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Seller seller)
		{
			if (!ModelState.IsValid)
			{
				var departments = await _departmentService.FindAllAsync();
				var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
				return View(viewModel);
			}

			if (id != seller.Id)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id mismatch!" });
			}

			try
			{
				await _sellerService.UpdateAsync(seller);
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
