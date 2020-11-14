using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhaoSat.Manager;
using Microsoft.AspNetCore.Builder;

namespace KhaoSat.API.Middleware
{
    public class CheckTokenMiddleware
    {
    //    private readonly RequestDelegate _next;
        
    //    public CheckTokenMiddleware(RequestDelegate next)
    //    {
    //        _next = next;

    //    }
    //    public async Task Invoke(HttpContext httpContext, IAccountsManager account)
    //    {
    //        this._account = account;
    //        var path = httpContext.Request.Path.Value;
    //        var token = httpContext.Request.Headers["token"].ToString();
    //        var checkAuthen = httpContext.Request.Headers["checktoken"].ToString();
    //        if (checkAuthen.ToLower() == "true")
    //        {

    //            if (string.IsNullOrEmpty(token))
    //            {
    //                await Task.Run(
    //                    async () =>
    //                    {
    //                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //                        await httpContext.Response.WriteAsync("Chưa có token");
    //                        return;
    //                    }
    //                );
    //            }
    //            var check = await TokenValidate(token);
    //            if (check == false)
    //            {
    //                await Task.Run(
    //                    async () =>
    //                    {
    //                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //                        await httpContext.Response.WriteAsync("Token không đúng hoặc đã hết hạn");
    //                        return;
    //                    }
    //                );

    //            }
    //            else
    //            {
    //                await _next(httpContext);
    //            }

    //        }
    //        else if (checkAuthen.ToLower() == "false")
    //        {
    //            await _next(httpContext);
    //        }
    //        else
    //        {
    //            await Task.Run(
    //                    async () =>
    //                    {
    //                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //                        await httpContext.Response.WriteAsync("");
    //                    }
    //                );
    //        }




    //    }


    //    private async Task<bool> TokenValidate(string token)
    //    {
    //        var data = await _account.CheckToken(token);
    //        if (data != null)
    //        {
    //            return true;
    //        }

    //        return false;
    //    }
    }
}
