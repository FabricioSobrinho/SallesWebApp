using SallesWebApp.Data;
using SallesWebApp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SallesWebApp.Services
{
	public class DepartmentService
	{
		private readonly SallesWebAppContext _context;

		public DepartmentService(SallesWebAppContext context)
		{
			_context = context;
		}

		public async Task<List<Department>> FindAllAsync()
		{
			return await _context.Department.OrderBy(x => x.Name).ToListAsync();
		}
	}
}
