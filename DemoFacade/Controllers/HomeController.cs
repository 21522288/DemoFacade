using DemoFacade.Models;
using DemoFacade.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoFacade.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocalFileStorageService _fileStorageService = new LocalFileStorageService();

       
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File không hợp lệ.");
            }    


            var fileUrl = await _fileStorageService.SaveFileAsync(file);
            ViewBag.FileUrl = $"{Request.Scheme}://{Request.Host}{fileUrl}";

            return View("Index");
        }

        [HttpGet]
        public IActionResult Visibility(string url) => View((object)url);

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
