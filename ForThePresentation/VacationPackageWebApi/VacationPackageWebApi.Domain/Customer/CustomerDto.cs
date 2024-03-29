﻿namespace VacationPackageWebApi.Domain.Customer;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}