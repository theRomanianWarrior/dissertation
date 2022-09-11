using HttpClients;
using Microsoft.AspNetCore.Mvc;
using VacationPackageWepApp.Models;

namespace VacationPackageWepApp.Controllers;

public class LoginController : Controller
{
    // GET
    public async Task<IActionResult> Index(Customer customer)
    {
        if (customer.Email == null)
        {
            if (ModelState.ContainsKey("InvalidLogin"))
            {
                ModelState.ClearValidationState("InvalidLogin");
            }
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userId");

            return View();
        }
        
        var customerPayload = customer.Email + ", " + customer.Password;
        var addressSuffix = "Customer/GetCustomerModel/" + customerPayload;

        using var preferencesPackageClient =
            new GenericRestfulCrudHttpClient<Customer, string>("http://localhost:7071/", "");

        var responseMessage = await preferencesPackageClient.GetAsync(addressSuffix);

        if (responseMessage == null)
        {
            ModelState.AddModelError("InvalidLogin", "Invalid Login Attempt");
            return View();
        }

        HttpContext.Session.SetString("userName", responseMessage.FirstName + " " + responseMessage.LastName);
        HttpContext.Session.SetString("userId", responseMessage.Id.ToString()!);
        return Redirect("/Home/Index");
    }
}