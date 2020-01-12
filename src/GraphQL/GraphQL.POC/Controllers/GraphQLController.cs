using GraphQL.POC.GrapgQL.Models;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GraphQL.POC.Controllers
{
    public class GraphQLController : ApiController
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(IDocumentExecuter documentExecuter, ISchema schema)
        {
            this._documentExecuter = documentExecuter;
            this._schema = schema;
        }

        [Route("get")]
        [HttpPost]
        public async Task<IActionResult> POST([FromBody]GraphQLQuery query)
        {
            var result = await _documentExecuter.ExecuteAsync((_) =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
            });
            return Ok(result);
        }

    }
}
