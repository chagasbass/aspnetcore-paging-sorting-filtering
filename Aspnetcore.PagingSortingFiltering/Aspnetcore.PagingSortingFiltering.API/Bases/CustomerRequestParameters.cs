namespace Aspnetcore.PagingSortingFiltering.API.Bases
{
    public class CustomerRequestParameters : RequestParameters
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; } = int.MaxValue;
        public string SearchTerm { get; set; }

        public bool ValidateAgeRange => MaxAge > MinAge;

        public CustomerRequestParameters()
        {
            OrderBy = "Name";
        }
    }
}
