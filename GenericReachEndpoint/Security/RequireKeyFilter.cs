using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GenericReachEndpoint.Security
{
    public class RequireKeyFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            
            if (_keys.All(x => x != headers["apikey"]))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private readonly IEnumerable<string> _keys = new List<string>
        {
            "rdGSYmdhDmpwbJ8Sq52qntVH", // Episerver uk
            "dTmp3R7MKmZrYGcTXKfYYTs4", // Haakon
            "965rbJNNqhwCVqEQgtDm58ey",
            "ZGXW239aAQPsWGgXryKqeQze",
            "urcbw9pyyKAtkrG6wjMRPkaw",
            "mULJtb6Z7FzGB2dev8aELMZp"
        };
    }
}