using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using webapp.Data;

namespace webapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseContext _context;

    public HomeController(ILogger<HomeController> logger, DatabaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult NewPolicy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewPolicy(PolicyModel policy) 
    {
        //todo add policy to database, delete policy in database and policy summary
        PolicyModel policyModel = new PolicyModel() {
            Id = policy.Id,
            insurerName = policy.insurerName,
            policyType = policy.policyType,
            startDate = policy.startDate,
            endDate = policy.endDate,
            premiumAmount = "0"

        };

        if (policy.policyType == "home") {
            policyModel.premiumAmount = "400";
        }
        else if (policy.policyType == "health") {
            policyModel.premiumAmount = "500";
        }
        else if (policy.policyType == "travel") {
            policyModel.premiumAmount = "100";
        }
        else if (policy.policyType == "auto") {
            policyModel.premiumAmount = "400";
        }

        await _context.Policies.AddAsync(policyModel);
        await _context.SaveChangesAsync();
        return RedirectToAction("NewPolicy");
    }

    public IActionResult Summary()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
