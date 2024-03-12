using System.Collections;

namespace SallesWebApp.Models.ViewModels
{
	public class SellerFormViewModel
	{
        public Seller? Seller { get; set; }
        public ICollection<Department>? Departments { get; set; }

		public SellerFormViewModel(Seller? seller, ICollection<Department>? departments)
		{
			Seller = seller;
			Departments = departments;
		}public SellerFormViewModel(ICollection<Department>? departments)
		{
			Departments = departments;
		}
	}
}
