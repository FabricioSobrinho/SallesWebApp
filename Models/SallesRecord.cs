using SallesWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SallesWebApp.Models
{
    public class SallesRecord
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Base salary")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Range(10, 1000000, ErrorMessage = "{0} must be between {1} and {2}")]
        [Required(ErrorMessage = "{0} is a required field")]
        public double Amount { get; set; }
        public SalleStatus Status { get; set; }
        public Seller? Seller { get; set; }

        public SallesRecord() { }

        public SallesRecord(int id, DateTime date, double amount, SalleStatus status, Seller? seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
