using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vangoph.Models;
using Vangoph.Reqeusts;
using Vangoph.Services;

namespace Vangoph.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MailSender _mailSender;

    public HomeController(ILogger<HomeController> logger, MailSender mailSender)
    {
        _logger = logger;
        _mailSender = mailSender;
    }

    public IActionResult Index()
    {
        return Redirect("/Image");
    }

    [HttpGet("/admin")]
    public IActionResult Admin()
    {
        return Redirect("/Image/manage");
    }

    public IActionResult Contact()
    {
        return View();
    }

     public IActionResult Shipping()
    {
        return View();
    }

    [HttpPost("/mail")]
    public async Task<IActionResult> SendEmail([FromForm] ContactRequest request)
    {
        if (ModelState.IsValid)
        {
            await _mailSender.SendAsync(request.Email, "Question", request.Description);
            return Redirect("Image");
        }

        return View("Error", "Attribute Invalid!");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("/Error", "Unknow Error");
    }
}