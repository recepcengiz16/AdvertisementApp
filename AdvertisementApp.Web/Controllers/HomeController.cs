using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Web.Extensions;
using AdvertisementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvertisementApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvidedService_Service _providedServiceManager;
        private readonly IAdvertisementService _advertisementService;

        public HomeController(IProvidedService_Service providedServiceManager, IAdvertisementService advertisementService)
        {
            _providedServiceManager = providedServiceManager;
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _providedServiceManager.GetAllAsync();
            return this.ResponseView(response);
        }
      
        public async Task<IActionResult> HumanResources()
        {
            var response =await _advertisementService.GetActivesAsync();
            return this.ResponseView(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}