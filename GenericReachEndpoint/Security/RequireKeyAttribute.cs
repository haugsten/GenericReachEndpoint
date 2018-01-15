using Microsoft.AspNetCore.Mvc;

namespace GenericReachEndpoint.Security
{
    public class RequireKeyAttribute : TypeFilterAttribute
    {
        public RequireKeyAttribute() : base(typeof(RequireKeyFilter))
        {
         
        }
    }
}