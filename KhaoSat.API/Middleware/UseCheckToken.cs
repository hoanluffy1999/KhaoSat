using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhaoSat.API.Middleware
{
    public static class CheckTokenExtensions
    {
        public static IApplicationBuilder UseCheckToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckTokenMiddleware>();
        }
        
    }
}
