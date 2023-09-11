using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
    //Generate ID based on last index of the given policy type
    public string GenerateSerial(string policyType) {
        var policy = _context.Policies
            .Where(x => x.policyType == policyType)
            .OrderByDescending(x => x.serialNumber)
            .FirstOrDefault();

        int temp = 1;
        
        if (policy != null) {
            int n = 1;

            if (policyType == "health" || policyType == "home") {
                n = 2;
            }
            if (int.TryParse(policy.serialNumber.Substring(n), out int numeric)) {
                temp = numeric + 1;
            }
        }

        string id = "";
        if (policyType == "health") {
            id = $"HE{temp:D4}";
        }
        else if (policyType == "home") {
            id = $"HO{temp:D4}";
        }
        else {
            id = $"{policyType.ToUpper()[0]}{temp:D4}";
        }

        return id;
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
        
        if (policy.startDate <= policy.endDate) {
            
            PolicyModel policyModel = new PolicyModel() {
                Id = policy.Id,
                serialNumber = GenerateSerial(policy.policyType),
                insurerName = policy.insurerName,
                policyType = policy.policyType,
                startDate = policy.startDate,
                endDate = policy.endDate,
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
        else {
            return RedirectToAction("NewPolicy");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Summary()
    {
        List < PolicyModel > policies = await _context.Policies.ToListAsync();

        return View(policies);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id) {
        
        var policy = await _context.Policies.FirstOrDefaultAsync(x => x.Id == id);

        if (policy != null) {
            var updatedPolicy = new UpdatePolicyModel() {
                Id = policy.Id,
                serialNumber = policy.serialNumber,
                insurerName = policy.insurerName,
                startDate = policy.startDate,
                endDate = policy.endDate,
                policyType = policy.policyType,
                premiumAmount = policy.premiumAmount
            };

            return View(updatedPolicy);
        }

        return RedirectToAction("Summary");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdatePolicyModel policyModel) {
        var policy = await _context.Policies.FindAsync(policyModel.Id);
        
        if (policy != null) {

            policy.insurerName = policyModel.insurerName;
            policy.startDate = policyModel.startDate;
            policy.endDate = policyModel.endDate;

            await _context.SaveChangesAsync();

            return RedirectToAction("Summary");
        }

        return RedirectToAction("Summary");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(UpdatePolicyModel policyModel) {
        var policy = await _context.Policies.FindAsync(policyModel.Id);

        if (policy != null) {
            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();

            return RedirectToAction("Summary");
        }

        return RedirectToAction("Summary");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
