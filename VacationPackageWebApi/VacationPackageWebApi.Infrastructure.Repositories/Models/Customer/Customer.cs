using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Customer
{
    public class Customer
    {
        public Customer()
        {
            ClientRequests = new HashSet<ClientRequest>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ClientRequest> ClientRequests { get; set; }
    }
}
