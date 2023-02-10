namespace VacationPackageWebApi.Domain.Customer.Contracts;

public interface ICustomerRepository
{
    public Task<CustomerDto?> GetCustomerModel(CustomerLoginRequest userData);
    public Task CreateNewCustomer(CustomerDto customer);
}