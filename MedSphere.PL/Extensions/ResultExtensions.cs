using MedSphere.BLL.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
namespace MedSphere.PL.Extensions;

public static class ResultExtensions
{
    public static ObjectResult ToProblem (this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot convert success result to a problem");
        }

        var problem = Results.Problem(statusCode: result.Error.StatusCode);


        var problemResult = problem as ProblemHttpResult;

        var details = problemResult?.ProblemDetails;

        details!.Extensions = new Dictionary<string, object?>
        {
            {
                "errors" , new[]
                {
                    new
                    {
                        result.Error.Code,
                        result.Error.Description
                    }
                }
            }
        };

        return new ObjectResult(details);


    }
}
