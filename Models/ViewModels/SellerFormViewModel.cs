using System.Collections;

namespace SallesWebApp.Models.ViewModels
{
	public class SellerFormViewModel
	{
        public Seller? Seller { get; set; }
        public ICollection<Department>? Departments { get; set; }
	}
}
