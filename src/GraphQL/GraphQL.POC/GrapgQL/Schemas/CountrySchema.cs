using GraphQL.POC.GrapgQL.Queries;
using GraphQL.Types;

namespace GraphQL.POC.GrapgQL.Schemas
{
    public class CountrySchema : Schema
    {
        public CountrySchema(IDependencyResolver resolver) : base(resolver)
        {
            this.Query = resolver.Resolve<CountryQuery>();
        }
    }
}
