using Microsoft.AspNetCore.Mvc;
using SallesWebApp.Models;
using System.Collections.Generic;

namespace SallesWebApp.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departments = new List<Department>();
            departments.Add(new Department{ Id = 21, Name = "Tech"});
            departments.Add(new Department{ Id = 12, Name = "Clothes"});

            return View(departments);
        }
    }
}
