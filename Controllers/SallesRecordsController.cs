using Microsoft.AspNetCore.Mvc;
using SallesWebApp.Services;

namespace SallesWebApp.Controllers
{
	public class SallesRecordsController : Controller
	{
		private readonly SallesRecordServices _sallesRecordServices;

		public SallesRecordsController(SallesRecordServices sallesRecordServices)
		{
			_sallesRecordServices = sallesRecordServices;
		}

		public IActionResult Index()
		{
			return View();
		}
		
		public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
		{
			if (!minDate.HasValue)
			{
				minDate = new DateTime(DateTime.Now.Year, 1, 1);
			}
			if (!maxDate.HasValue)
			{
				maxDate = DateTime.Now;
			}

			ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
			ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

			var result = await _sallesRecordServices.FindByDateAsync(minDate, maxDate);
			return View(result);
		}
		public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
		{
			if (!minDate.HasValue)
			{
				minDate = new DateTime(DateTime.Now.Year, 1, 1);
			}
			if (!maxDate.HasValue)
			{
				maxDate = DateTime.Now;
			}

			ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
			ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

			var result = await _sallesRecordServices.FindByDateGroupingAsync(minDate, maxDate);
			return View(result);
		}
	}
}
