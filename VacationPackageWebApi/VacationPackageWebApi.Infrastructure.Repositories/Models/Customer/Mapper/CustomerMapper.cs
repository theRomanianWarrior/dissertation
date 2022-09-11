using VacationPackageWebApi.Domain.Customer;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Customer.Mapper;

public static class CustomerMapper
{
    public static CustomerDto ToEntity(this Customer customer)
    {
        return new CustomerDto()
        {
            Id = customer.Id,
            Email = customer.Email,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Login = customer.Login
        };
    }
    
    public static Customer ToDatabaseModel(this CustomerDto customer)
    {
        return new Customer()
        {
            Id = customer.Id,
            Email = customer.Email,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Login = customer.Login
        };
    }
}