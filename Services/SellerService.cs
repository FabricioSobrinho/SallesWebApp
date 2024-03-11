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
			_context.Add(seller);
			_context.SaveChanges();
		}

		public Seller FindById(int id)
		{
			return _context.Seller.FirstOrDefault(x => x.Id == id);
		}

		public void Remove(int id)
		{
			var seller = _context.Seller.Find(id);
			_context.Seller.Remove(seller!);
			_context.SaveChanges();
		}
	}
}
