using ACS_Backend.Model;

namespace ACS_Backend.Middlewares;

using Exceptions;

public class GlobalExceptionHandling : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var res = new GenericResponseModel<string>() { QueryIsSuccess = false };
        try
        {
            await next(context);
        }
        catch (FailedLoginException)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Failed login");
        }
        catch (ItemNotFoundException)
        {
            res.Message = "Item not found";
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (UnprocessableEntityException)
        {
            res.Message = "Unprocessable entity";
            context.Response.StatusCode = 422;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (BadFormatException)
        {
            res.Message = "Bad format";
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (ReferredEntityNotFoundException)
        {
            res.Message = "Referred entity not found";
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
            res.Message = string.Join(", ", e.FailedOn);
            context.Response.StatusCode = 409;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (ArgumentException e)
        {
            res.Message = e.Message;
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (ItemAlreadyExistsException)
        {
            res.Message = "Item already exists";
            context.Response.StatusCode = 409;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (NotAddedException)
        {
            res.Message = "Not added";
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(res);
        }
        catch (Exception e)
        {
            res.Message = e.Message;
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(res);
        }
    }
}