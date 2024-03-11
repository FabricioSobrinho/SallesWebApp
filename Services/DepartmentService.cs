using SallesWebApp.Data;
using SallesWebApp.Models;

namespace SallesWebApp.Services
{
	public class DepartmentService
	{
		private readonly SallesWebAppContext _context;

		public DepartmentService(SallesWebAppContext context)
		{
			_context = context;
		}

		public List<Department> FindAll()
		{
			return _context.Department.OrderBy(x => x.Name).ToList();
		}
	}
}
