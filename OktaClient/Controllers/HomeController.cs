using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OktaClient.Models;
using OktaClient.Services;

namespace OktaClient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookApiService _bookApiService;

    public HomeController(ILogger<HomeController> logger, BookApiService bookApiService)
    {
        _logger = logger;
        _bookApiService = bookApiService;
    }

    public IActionResult Index()
    {
        var response = _bookApiService.GetBooks().Result;

        var viewModel = new BooksViewModel()
        {
            Books = response
        };
        return View(viewModel);
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