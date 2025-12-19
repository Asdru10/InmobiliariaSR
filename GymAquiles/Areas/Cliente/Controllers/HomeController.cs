using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace ProyectoInmobilaria.Areas.Customer.Controllers
{

    [Area("Cliente")]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
           return View();
        }


       

    }
}
