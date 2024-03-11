using SallesWebApp.Data;
using SallesWebApp.Models;

namespace SallesWebApp.Services
{
	public class SellerService
	{
		private readonly SallesWebAppContext _context;

		public SellerService(SallesWebAppContext context)
		{
			_context = context;
		}

		public List<Seller> FindAll()
		{
			return _context.Seller.ToList();
		}
		public void Insert(Seller seller)
		{
			seller.Department = _context.Department.First();
			_context.Add(seller);
			_context.SaveChanges();
		}
	}
}
