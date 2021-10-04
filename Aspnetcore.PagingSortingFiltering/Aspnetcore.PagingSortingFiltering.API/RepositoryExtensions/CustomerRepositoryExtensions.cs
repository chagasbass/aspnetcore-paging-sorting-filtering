using Aspnetcore.PagingSortingFiltering.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Aspnetcore.PagingSortingFiltering.API.RepositoryExtensions
{
    public static class CustomerRepositoryExtensions
    {
        /// <summary>
        /// Trocar para IQueryable com EFCore
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <returns></returns>
        public static IEnumerable<Customer> FilterCustomers(this IEnumerable<Customer> customers, int minAge, int maxAge)
        {
            return customers.Where(e => (e.Age >= minAge && e.Age <= maxAge));
        }

        /// <summary>
        /// /// Trocar para IQueryable com EFCore
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IEnumerable<Customer> Search(this IEnumerable<Customer> customers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return customers;
            }

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return customers.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        /// <summary>
        /// Extension para uso de order by usando o pacote  System.Linq.Dynamic.Core;
        /// Trocar para Iqueryable ao usar o EfCore
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="orderByQueryString"></param>
        /// <returns></returns>
        public static IEnumerable<Customer> Sort(this IEnumerable<Customer> customers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return customers.OrderBy(e => e.Name);
            }

            var orderParams = orderByQueryString.Trim().Split(',');

            var propertyInfos = typeof(Customer).GetProperties(BindingFlags.Public |
                                                               BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];

                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                                                                       pi.Name.Equals(propertyFromQueryName,
                                                                                      StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty is null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
                return customers.OrderBy(e => e.Name);

            //hard code por causa do IEnumerable mas deve retornar a linha de baixo.
            return customers.OrderBy(c => c.Name);

            //return customers.OrderBy(orderQuery);
        }
    }
}
