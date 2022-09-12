using Microsoft.AspNetCore.Mvc;
using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Customer;

namespace VacationPackageWebApi.API.Controllers;

[Route("[controller]/[action]")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{userData}")]
    public async Task<CustomerDto?> GetCustomerModel(string userData)
    {
        return await _customerService.GetCustomerModel(userData);
    }

    [HttpPost]
    public async Task<ActionResult> CreateNew([FromBody] CustomerDto customer)
    {
        await _customerService.CreateNewCustomer(customer);
        return new JsonResult("Success");
    }
}