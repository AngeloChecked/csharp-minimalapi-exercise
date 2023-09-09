using Newtonsoft.Json.Linq;

namespace MinimalApi;

class WebServer
{
    private IDataRepository dataRepository;

    public WebServer(IDataRepository dataRepository)
    {
        this.dataRepository = dataRepository;
    }

    public void Run()
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.UseStaticFiles();
        app.UseDefaultFiles();
        app.Map("/", context => Task.Run((() => context.Response.Redirect("/index.html"))));

        app.MapGet("/item", async context =>
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JObject.FromObject(dataRepository.GetData()).ToString());
        });
        app.MapPost("/item/{id}/delete", async ([Microsoft.AspNetCore.Mvc.FromRoute] String id, HttpResponse response) =>
        {
            dataRepository.DeleteById(id);
            response.StatusCode = 202;
            response.ContentType = "application/json";
            await response.WriteAsync("""{ "id":""" + '"' + id + '"' + " }");
        });

        app.UseWhen(
            context => context.Request.Path.StartsWithSegments("/items"), appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    if (context.Request.Headers.Authorization == "Bearer secret")
                    {
                        await next();
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Unauthorized");
                    }
                });
            }
        );
        app.Run();
    }

}

