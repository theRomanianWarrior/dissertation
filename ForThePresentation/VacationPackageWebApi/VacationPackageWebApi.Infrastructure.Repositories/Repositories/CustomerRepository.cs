using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.Customer;
using VacationPackageWebApi.Domain.Customer.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Customer.Mapper;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly VacationPackageContext _context;

    public CustomerRepository(VacationPackageContext context)
    {
        _context = context;
    }

    public async Task<CustomerDto?> GetCustomerModel(CustomerLoginRequest userData)
    {
        var customer =
            await _context.Customers.SingleOrDefaultAsync(c =>
                c.Email == userData.email && c.Password == userData.password);

        return customer?.ToEntity();
    }

    public async Task CreateNewCustomer(CustomerDto customer)
    {
        await _context.Customers.AddAsync(customer.ToDatabaseModel());
        await _context.SaveChangesAsync();
    }
}