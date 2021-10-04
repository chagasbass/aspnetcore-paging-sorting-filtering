using Aspnetcore.PagingSortingFiltering.API.Bases;
using Aspnetcore.PagingSortingFiltering.API.Entities;
using System.Threading.Tasks;

namespace Aspnetcore.PagingSortingFiltering.API.Repositories
{
    public interface ICustomerRepository
    {
        Task<PagedList<Customer>> GetCustomersByFiltersAsync(CustomerRequestParameters customerRequestParameters);
    }
}
