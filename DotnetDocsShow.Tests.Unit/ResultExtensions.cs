using System;
using Microsoft.AspNetCore.Http;

namespace DotnetDocsShow.Tests.Unit;

public static class ResultExtensions
{
    public static T? GetOkObjectResultValue<T>(this IResult result)
    {
        return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("Value",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetOkObjectResultStatusCode(this IResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetNotFoundResultStatusCode(this IResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }
}
