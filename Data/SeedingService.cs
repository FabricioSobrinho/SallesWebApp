
using SallesWebApp.Models;

namespace SallesWebApp.Data
{
    public class SeedingService
    {
        private SallesWebAppContext _context;

        public SeedingService(SallesWebAppContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SallesRecord.Any())
            {
                return;
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Louise Corso", "louu@gmail.com", new DateTime(2004, 10, 29), 2400, d3);
            Seller s2 = new Seller(2, "João Victor Cavalcante", "jv@gmail.com", new DateTime(2003, 05, 21), 2200, d4);
            Seller s3 = new Seller(3, "Danilo Otário", "brasilia@gmail.com", new DateTime(2003, 03, 05), 3000, d1);
            Seller s4 = new Seller(4, "Mikael Javier Feitosa", "mika@gmail.com", new DateTime(2002, 10, 10), 2400, d2);
            Seller s5 = new Seller(5, "Roger Duarte", "rogi@gmail.com", new DateTime(2005, 12, 18), 1800, d2);
            Seller s6 = new Seller(6, "Talita Ribeiro", "talee@gmail.com", new DateTime(1995, 01, 10), 2400, d2);

            SallesRecord sr1 = new SallesRecord(21, new DateTime(2022, 05, 21), 4000, Models.Enums.SalleStatus.Billed, s1);
            SallesRecord sr2 = new SallesRecord(9, new DateTime(2022, 11, 23), 30000, Models.Enums.SalleStatus.Canceled, s2);
            SallesRecord sr3 = new SallesRecord(23, new DateTime(2023, 03, 01), 4000, Models.Enums.SalleStatus.Pending, s3);
            SallesRecord sr4 = new SallesRecord(54, new DateTime(2022, 07, 21), 3700, Models.Enums.SalleStatus.Billed, s6);
            SallesRecord sr5 = new SallesRecord(27, new DateTime(2022, 11, 10), 10000, Models.Enums.SalleStatus.Pending, s5);
            SallesRecord sr6 = new SallesRecord(81, new DateTime(2019, 11, 23), 9200, Models.Enums.SalleStatus.Billed, s4);

            _context.Department.AddRange(d1, d2, d3, d4);

            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);

            _context.SallesRecord.AddRange(sr1, sr2, sr3, sr4, sr5, sr6);

            _context.SaveChanges();
        }

    }
}
