using HttpClients;
using Microsoft.AspNetCore.Mvc;
using VacationPackageWepApp.Models;

namespace VacationPackageWepApp.Controllers;

public class SignUpController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterNew(Customer? customer)
    {
        if (customer == null) return new ViewResult();
        using var preferencesPackageClient =
            new GenericRestfulCrudHttpClient<Customer, string>("http://localhost:7071/", "Customer/CreateNew");

        customer.Id = Guid.NewGuid();
        await preferencesPackageClient.PostAsync<string>(customer);
        return Redirect("/Login/Index");
    }
}