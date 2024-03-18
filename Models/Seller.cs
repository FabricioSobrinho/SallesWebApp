using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SallesWebApp.Models
{
	public class Seller
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} is a required field")]
		[StringLength(60, MinimumLength = 10, ErrorMessage = "{0} size should be between {2} and {1} chars")]
		public string? Name { get; set; }

		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "{0} is a required field")]
		public string? Email { get; set; }

		[Display(Name = "Birth date")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Base salary")]
		[DisplayFormat(DataFormatString = "{0:C2}")]
		[Range(10, 1000000, ErrorMessage = "{0} must be between {1} and {2}")]
		[Required(ErrorMessage = "{0} is a required field")]
		public double BaseSalary { get; set; }
		public Department? Department { get; set; }
		public int DepartmentId { get; set; }
		public ICollection<SallesRecord> Salles { get; set; } = new List<SallesRecord>();

		public Seller() { }

		public Seller(int id, string? name, string? email, DateTime birthDate, double baseSalary, Department? department)
		{
			Id = id;
			Name = name;
			Email = email;
			BirthDate = birthDate;
			BaseSalary = baseSalary;
			Department = department;
		}

		public void AddSales(SallesRecord sr)
		{
			Salles.Add(sr);
		}
		public void RemoveSales(SallesRecord sr)
		{
			Salles.Remove(sr);
		}
		public double TotalSalles(DateTime initial, DateTime final)
		{
			var totalSalles = Salles.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);

			return totalSalles;
		}
	}
}
