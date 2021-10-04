using System;

namespace Aspnetcore.PagingSortingFiltering.API.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Customer() { }

    }
}
