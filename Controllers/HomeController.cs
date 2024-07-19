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

    [HttpPost("/contactadmin")]
    public async Task<IActionResult> ContactAdminAsync([FromForm] ContactRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View("Error", ModelState.Values.SelectMany(it => it.Errors).Select(e => e.ErrorMessage).ToList());
        }

        await _mailSender.NotifyAdmin(request);
        await _mailSender.ReplyUser(request);
        return Redirect("/Image/manage");
    }

    [HttpPost("/shipnotify")]
    public async Task<IActionResult> ShipNotifyAsync([FromForm] ShipNotifyRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View("Error", ModelState.Values.SelectMany(it => it.Errors).Select(e => e.ErrorMessage).ToList());
        }

        await _mailSender.ShipNotifyAsync(request);
        return Redirect("/Image/manage");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("/Error", new List<string> { "Unknow Error" });
    }
}