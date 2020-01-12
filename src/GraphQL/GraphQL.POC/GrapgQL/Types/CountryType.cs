using GraphQL.POC.Repo;
using GraphQL.Types;

namespace GraphQL.POC.GrapgQL.Models
{
    public class CountryType : ObjectGraphType<RestCountriesResponseModel>
    {
        public CountryType()
        {
            Name = "Country";
            Field(p => p.Name).Description("Name of the Country");
            Field(p => p.Capital).Description("Capital of the Country");
            Field(p => p.Alpha2Code).Description("Alpha2Code  of the Country");
            Field(p => p.Alpha3Code).Description("Alpha3Code  of the Country");
        }
    }
}
