using System.Collections.Generic;

namespace SallesWebApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public void RemoveSeller(Seller seller)
        {
            Sellers.Remove(seller);
        }

        public double TotalSalles(DateTime initial, DateTime final)
        {
            var totalSalles = Sellers.Sum(seller => seller.TotalSalles(initial, final));

            return totalSalles;
        }
    }
}
