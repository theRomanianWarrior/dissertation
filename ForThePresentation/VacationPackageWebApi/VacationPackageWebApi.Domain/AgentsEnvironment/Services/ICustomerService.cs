using VacationPackageWebApi.Domain.Customer;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface ICustomerService
{
    public Task<CustomerDto?> GetCustomerModel(string userData);
    public Task CreateNewCustomer(CustomerDto customer);
}