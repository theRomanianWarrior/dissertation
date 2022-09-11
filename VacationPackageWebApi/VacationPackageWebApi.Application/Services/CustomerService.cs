using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Customer;
using VacationPackageWebApi.Domain.Customer.Contracts;

namespace VacationPackageWebApi.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<CustomerDto?> GetCustomerModel(string userData)
    {
        var customerData = userData.Split(", ");
        return await _customerRepository.GetCustomerModel( new CustomerLoginRequest()
        {
            email = customerData[0],
            password = customerData[1]
        });
    }

    public async Task CreateNewCustomer(CustomerDto customer)
    {
        await _customerRepository.CreateNewCustomer(customer);
    }
}