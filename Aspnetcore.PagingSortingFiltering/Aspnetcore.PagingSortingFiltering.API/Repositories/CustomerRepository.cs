using Aspnetcore.PagingSortingFiltering.API.Bases;
using Aspnetcore.PagingSortingFiltering.API.Entities;
using Aspnetcore.PagingSortingFiltering.API.RepositoryExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspnetcore.PagingSortingFiltering.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> Customers { get; private set; }

        public CustomerRepository()
        {
            CreateCustomers();
        }

        private void CreateCustomers()
        {
            Customers = new List<Customer>();

            string customerName = "Customer";

            for (int iCustomer = 0; iCustomer < 100; iCustomer++)
            {
                Customers.Add(new()
                {
                    Age = iCustomer + 18,
                    Id = Guid.NewGuid(),
                    Name = $"{customerName}-{iCustomer}"
                });
            }
        }

        public async Task<PagedList<Customer>> GetCustomersByFiltersAsync(CustomerRequestParameters customerRequestParameters)
        {
            var filteredCustomers = Customers.FilterCustomers(customerRequestParameters.MinAge, customerRequestParameters.MaxAge)
                                             .Search(customerRequestParameters.SearchTerm)
                                             .Sort(customerRequestParameters.OrderBy)
                                             .ToList();

            return PagedList<Customer>.ToPagedList(filteredCustomers,
                                                   customerRequestParameters.PageNumber,
                                                   customerRequestParameters.PageSize);
        }
    }
}
