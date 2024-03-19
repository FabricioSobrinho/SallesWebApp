using SallesWebApp.Data;
using SallesWebApp.Models;
using SallesWebApp.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace SallesWebApp.Services
{
	public class SellerService
	{
		private readonly SallesWebAppContext _context;

		public SellerService(SallesWebAppContext context)
		{
			_context = context;
		}

		public async Task<List<Seller>> FindAllAsync()
		{
			return await _context.Seller.ToListAsync();
		}
		public async Task InsertAsync(Seller seller)
		{
			_context.Add(seller);
			await _context.SaveChangesAsync();
		}

		public async Task<Seller> FindByIdAsync(int id)
		{
			return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task RemoveAsync(int id)
		{
			var seller = await _context.Seller.FindAsync(id);
			_context.Seller.Remove(seller!);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Seller seller)
		{
			var hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);
			if (!hasAny)
			{
				throw new NotFoundException("Id not found");
			}
			try
			{
				_context.Update(seller);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw new DbConcurrencyException(ex.Message);
			}
		}
	}
}
