using SallesWebApp.Data;
using SallesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SallesWebApp.Services
{
	public class SallesRecordServices
	{
		private readonly SallesWebAppContext _context;

		public SallesRecordServices(SallesWebAppContext context)
		{
			_context = context;
		}

		public async Task<List<SallesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
		{

			var result = from obj in _context.SallesRecord select obj;
			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);
			}

			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);
			}

			return await result.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.ToListAsync();
		}
		public async Task<List<IGrouping<Department,SallesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
		{

			var result = from obj in _context.SallesRecord select obj;
			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);
			}

			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);
			}

			return await result.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.GroupBy(x => x.Seller.Department)
				.ToListAsync();
		}
	}
}
